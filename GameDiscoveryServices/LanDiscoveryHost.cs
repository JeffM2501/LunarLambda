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

        public static void Startup()
        {
            if (SocketHost != null)
                return;

            SocketHost = new TcpListener(IPAddress.Any, DiscoveryPort);

            SocketHost.Start();
            SocketHost.BeginAcceptTcpClient(AcceptRequest, null);
        }

        public static void Shutdown()
        {
            if (SocketHost != null)
            {
                SocketHost.Stop();
            }

            SocketHost = null;
        }

        private static void AcceptRequest(IAsyncResult result)
        {
            var client = SocketHost.EndAcceptTcpClient(result);
            SocketHost.BeginAcceptTcpClient(AcceptRequest, null);

            if (ServiceList.LocalService == null)
                client.Close();
            else
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(HostedService));
                serializer.WriteObject(client.GetStream(), ServiceList.LocalService);
                client.GetStream().Flush();
                client.Close();
            }
        }
    }
}
