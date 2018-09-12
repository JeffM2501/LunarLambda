using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace GameDiscoveryServices
{
    public static class LANDiscoveryClient
    {
        public class LANHost
        {
            public IPAddress LANAddress = null;
            private readonly TcpClient Client = null;

            private bool IsDone = false;
            private bool IsError = false;

            public HostedService ServiceResult = null;

            public bool Done { get { lock (Client) return IsDone; } }
            public bool Error { get { lock (Client) return IsError; } }

            public LANHost(IPAddress address)
            {
                LANAddress = address;

                Client = new TcpClient();
                Client.BeginConnect(address, 1700, Connected, null);
            }

            protected void Connected(IAsyncResult result)
            {
                try
                {
                    Client.EndConnect(result);
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(HostedService));
                    ServiceResult = serializer.ReadObject(Client.GetStream()) as HostedService;
                    Client.Close();

                    lock (Client)
                    {
                        IsDone = true;
                        IsError = ServiceResult == null;
                    }
                }
                catch (Exception)
                {

                    lock (Client)
                    {
                        IsDone = true;
                        IsError = true;
                    }
                }
            }
        }

        private static List<LANHost> ActiveScanHosts = new List<LANHost>();
        private static List<IPAddress> PendingAddresses = new List<IPAddress>();

        private static Thread WorkerThread = null;

        private static int MaxInteregations = 2;

        static LANDiscoveryClient()
        {
            LanUDPPinger.NewHostFound += LanUDPPinger_NewHostFound;
        }

        public static void Shutdown()
        {
            if (WorkerThread != null)
                WorkerThread.Abort();

            WorkerThread = null;
        }

        private static void LanUDPPinger_NewHostFound(object sender, EventArgs e)
        {
            lock (PendingAddresses)
            {
                foreach (var address in LanUDPPinger.FoundAddresses)
                {
                    if (PendingAddresses.Find((x) => x.Equals(address)) == null)
                        PendingAddresses.Add(address);
                }
            }

			CheckThread();
		}

        public static void StartScan()
        {
            LanUDPPinger.StartPing();
        }

        private static void CheckThread()
        {
            lock (PendingAddresses)
            {
                if (WorkerThread != null || PendingAddresses.Count == 0)
                    return;
            }

            WorkerThread = new Thread(new ThreadStart(CheckHosts));
            WorkerThread.Start();
        }

        private static IPAddress PopAddress()
        {
            lock (PendingAddresses)
            {
                IPAddress addy = null;
                if (PendingAddresses.Count > 0)
                {
                    addy = PendingAddresses[0];
                    PendingAddresses.RemoveAt(0);
                }
                return addy;
            }
        }

        private static void CheckHosts()
        {
            bool done = false;
            while (!done)
            {
                int pendingCount = 0;
                lock (PendingAddresses)
                    pendingCount = PendingAddresses.Count;

                while (ActiveScanHosts.Count < MaxInteregations && pendingCount > 0)
                    ActiveScanHosts.Add(new LANHost(PopAddress()));

                foreach (var host in ActiveScanHosts.ToArray())
                {
                    if (host.Done)
                    {
                        ActiveScanHosts.Remove(host);

                        if (!host.Error)
                            ServiceList.AddRemoteService(host.ServiceResult);
                    }
                }

                Thread.Sleep(100);

                pendingCount = 0;
                lock (PendingAddresses)
                    pendingCount = PendingAddresses.Count;

                done = ActiveScanHosts.Count == 0 && pendingCount == 0;
            }

            WorkerThread = null;
        }
    }
}
