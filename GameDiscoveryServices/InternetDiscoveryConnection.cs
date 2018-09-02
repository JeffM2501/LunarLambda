using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace GameDiscoveryServices
{
	/// <summary>
	/// Connects to a global master server and gets remote WAN services
	/// </summary>
	public static class InternetDiscoveryConnection
	{
		public static string DefaultServiceURL = "https://services.hyperdrive.tech/ll/";
		internal static WebClient CurrentWebClient = null;
		internal static Thread WorkerThread = null;

		internal static List<HostedService> PendingPublicUpdates = new List<HostedService>();
		internal static bool PendingPull = false;

		internal static void PushPublicSevice(HostedService service)
		{
			PendingPublicUpdates.Add(service);
			CheckThread();
		}

		internal static void CheckThread()
		{
			lock(PendingPublicUpdates)
			{
				if (PendingPublicUpdates.Count == 0 || !PendingPull || WorkerThread != null)
					return;

				WorkerThread = new Thread(new ThreadStart(CheckOnline));
				WorkerThread.Start();
			}
		}

		private static HostedService PopPendingUpdate()
		{
			lock(PendingPublicUpdates)
			{
				if (PendingPublicUpdates.Count == 0)
					return null;
				HostedService s = PendingPublicUpdates[0];
				PendingPublicUpdates.RemoveAt(0);

				return s;
			}
		}

		private static void CheckOnline()
		{
			if (CurrentWebClient == null)
				CurrentWebClient = new WebClient();

			DataContractJsonSerializer updateSerializer = new DataContractJsonSerializer(typeof(HostedService));
			HostedService update = PopPendingUpdate();
			while (update != null)
			{
				try
				{
					MemoryStream ms = new MemoryStream();
					updateSerializer.WriteObject(ms, update);
					ms.Close();
					CurrentWebClient.UploadData(DefaultServiceURL + "action=update", ms.ToArray());
				}
				catch (Exception)
				{

					// ignore web exceptions, we'll get it on the next update I guess
				}

				update = PopPendingUpdate();
			}

			bool pull = false;
			lock (PendingPublicUpdates)
			{
				pull = PendingPull;
				PendingPull = false;
			}

			if (pull)
			{
				DataContractJsonSerializer pullSerialzier = new DataContractJsonSerializer(typeof(HostedServicesList));

				try
				{
					HostedServicesList list = pullSerialzier.ReadObject(CurrentWebClient.OpenRead(DefaultServiceURL + "action=get")) as HostedServicesList;
					if (list != null)
					{
						if (list.StructureVersion == 1)
						{
							foreach (var host in list.Services)
								ServiceList.AddRemoteService(host);
						}
					}
				}
				catch (Exception)
				{

					// ignore web exceptions, we'll get it on the next update I guess
				}
		
			}
		}
	}

}
