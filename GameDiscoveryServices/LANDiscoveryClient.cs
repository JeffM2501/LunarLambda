using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace GameDiscoveryServices
{
    public static class LANDiscoveryClient
    {
        public class LANHost
        {
            public IPAddress LANAddress = null;
            public TcpClient Client = null;
        }

        private static List<LANHost> ActiveScanHosts = new List<LANHost>();
        private static List<IPAddress> PendingAddresses = new List<IPAddress>();
    }
}
