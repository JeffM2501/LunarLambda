using System;
using System.Collections.Generic;

using System.Net;
using System.Net.Sockets;

namespace GameDiscoveryServices
{
    public static class LanUDPPinger
    {
        private static List<IPAddress> RespondedHosts = new List<IPAddress>();

        private static UdpClient UDPPinger = new UdpClient();

        public static event EventHandler NewHostFound = null;

        public static bool HaveResults()
        {
            lock (RespondedHosts)
                return RespondedHosts.Count > 0;
        }

        public static IPAddress[] FoundAddresses { get { lock (RespondedHosts) return RespondedHosts.ToArray(); } }

        public static void StartPing()
        {
            lock (RespondedHosts)
                RespondedHosts.Clear();

            UDPPinger.EnableBroadcast = true;
            byte[] payload = new byte[] { (byte)'l', (byte)'l', 0, (byte)HostedService.DefaultStructureVersion };
            UDPPinger.Send(payload, 4, new IPEndPoint(IPAddress.Broadcast, 1700));
            UDPPinger.BeginReceive(GetPingback, null);
        }

        private static void GetPingback(IAsyncResult result)
        {
            IPEndPoint remote = null;
            byte[] data = UDPPinger.EndReceive(result, ref remote);
            UDPPinger.BeginReceive(GetPingback, null);

            if (data.Length == 4 && data[0] == 'l' && data[1] == 'l' && data[2] == 0 && data[3] == (byte)HostedService.DefaultStructureVersion)
            {
                bool call = false;
                lock(RespondedHosts)
                {
                    if (RespondedHosts.Find((x)=> x.Equals(remote.Address)) == null)
                    {
                        RespondedHosts.Add(remote.Address);
                        call = true;
                    }
                }

                if (call)
                    NewHostFound?.Invoke(null, null);
            }
        }
    }
}
