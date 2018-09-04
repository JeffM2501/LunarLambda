using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDiscoveryServices
{
    public class HostedService
    {
		public int StructureVersion = 1;

		public string Name = string.Empty;
		public string TypeID = string.Empty;
		public string Key = string.Empty;

		public string LANAddress = string.Empty;
		public string WANAddress = string.Empty;

		public int Port = 1701;
		public bool Secured = false;

		public bool PossibleLANService = false;

		internal bool IsLocal = false;
		internal bool WasPublished = false;
		internal string GlobalAccessKey = string.Empty;

		public List<Tuple<string, string>> Properties = new List<Tuple<string, string>>();

		public List<Tuple<string, HostedService>> KnownOtherServices = new List<Tuple<string, HostedService>>();
    }

	public class HostedServicesList
	{
		public int StructureVersion = 1;

		public List<HostedService> Services = new List<HostedService>();
	}
}
