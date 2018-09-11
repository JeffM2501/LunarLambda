using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace GameDiscoveryServices
{
    public static class LANDiscoveryHost
    {
        private static readonly int DiscoveryPort = 1700;

        private static TcpListener SocketHost = null;
        private static UdpClient UDPPingResponder = null;

        public static bool Startup()
        {
            if (SocketHost != null)
                return false;

            SocketHost = new TcpListener(IPAddress.Any, DiscoveryPort);

            SocketHost.Start();
            SocketHost.BeginAcceptTcpClient(AcceptRequest, null);

            UDPPingResponder = new UdpClient(1700);
            UDPPingResponder.EnableBroadcast = true;
            UDPPingResponder.BeginReceive(HandleUDPPing, null);

            return true;
        }

        public static void Shutdown()
        {
            if (SocketHost != null)
                SocketHost.Stop();

            SocketHost = null;

            if (UDPPingResponder != null)
                UDPPingResponder.Close();
            UDPPingResponder = null;
        }

        private static void AcceptRequest(IAsyncResult result)
        {
            if (SocketHost == null)
                return;

            var client = SocketHost.EndAcceptTcpClient(result);
            SocketHost.BeginAcceptTcpClient(AcceptRequest, null);

            if (ServiceList.LocalService == null)
                client.Close();
            else
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(HostedService));
                serializer.WriteObject(client.GetStream(), ServiceList.LocalService);
                client.GetStream().Write(new byte[] { 0, 0, 0, 0 }, 0, 4);
                client.GetStream().Flush();
                client.Close();
            }
        }

        private static void HandleUDPPing (IAsyncResult result)
        {
            if (UDPPingResponder == null)
                return;

            IPEndPoint client = null;
            byte[] data = UDPPingResponder.EndReceive(result, ref client);
            UDPPingResponder.BeginReceive(HandleUDPPing, null);

            if (data.Length == 4 && data[0] == 'l' && data[1] == 'l' && data[2] == 0 && data[3] == (byte)HostedService.DefaultStructureVersion)
            {
                byte[] payload = new byte[] { (byte)'l', (byte)'l', 0, (byte)HostedService.DefaultStructureVersion };
                UDPPingResponder.Send(payload, 4, client);
            }
        }
    }
}
