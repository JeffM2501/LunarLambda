using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

namespace GameDiscoveryServices
{
	/// <summary>
	/// Connects to a global master server and gets remote WAN services
	/// </summary>
	public static class InternetDiscoveryConnection
	{
		public static string DefaultServiceURL = "https://services.hyperdrive.tech/ll/";
		private static WebClient CurrentWebClient = null;
		private static Thread WorkerThread = null;

		private static HostedService ServiceToUpdate = null;
		private static HostedService ServiceToRemove = null;

		public static int ServiceUpdateIntervalSeconds = 300;

		public enum PendingActions
		{
			Nothing,
			Push,
			Pull,
			Remove,
		}
		private static List<PendingActions> Pending = new List<PendingActions>();

		internal static void PushPublicSevice(HostedService service)
		{
			if (ServiceToUpdate != null)
			{
				ServiceToRemove = ServiceToUpdate;
				lock (Pending)
					Pending.Add(PendingActions.Remove);
			}
			service.WasPublished = true;
			ServiceToUpdate = service;
			lock (Pending)
				Pending.Add(PendingActions.Push);
			CheckThread();
		}

		internal static void RemovePublisService()
		{
			if (ServiceToUpdate != null)
			{
				ServiceToRemove = ServiceToUpdate;
				lock (Pending)
					Pending.Add(PendingActions.Remove);
				ServiceToUpdate = null;
				CheckThread();
			}
		}

		internal static void CheckThread()
		{
			lock(Pending)
			{
				if (Pending.Count == 0 || WorkerThread != null)
					return;

				WorkerThread = new Thread(new ThreadStart(CheckOnline));
				WorkerThread.Start();
			}
		}

		private static PendingActions PopPending()
		{
			lock(Pending)
			{
				if (Pending.Count == 0)
					return PendingActions.Nothing;
				PendingActions s = Pending[0];
				Pending.RemoveAt(0);

				return s;
			}
		}

		private static void CheckOnline()
		{
			if (CurrentWebClient == null)
				CurrentWebClient = new WebClient();

			
			PendingActions action = PopPending();
			while (action != PendingActions.Nothing)
			{

				switch (action)
				{
					case PendingActions.Pull:
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

							ServiceList.CallUpdateNotifiation();
						}
						break;

					case PendingActions.Push:
						try
						{
							DataContractJsonSerializer updateSerializer = new DataContractJsonSerializer(typeof(HostedService));

							MemoryStream ms = new MemoryStream();
							updateSerializer.WriteObject(ms, ServiceToUpdate);
							ms.Close();
							byte[] responce = CurrentWebClient.UploadData(DefaultServiceURL + "?action=update", ms.ToArray());
							ServiceToUpdate.GlobalAccessKey = Encoding.UTF8.GetString(responce);
						}
						catch (Exception)
						{

							// ignore web exceptions, we'll get it on the next update I guess
						}
						break;

					case PendingActions.Remove:
						if (ServiceToRemove == null)
							break;
						try
						{
							CurrentWebClient.UploadString(DefaultServiceURL + "?action=remove", ServiceToRemove.GlobalAccessKey);
						}
						catch (Exception)
						{

							// ignore web exceptions, it'll time out
						}

						lock (Pending)
							ServiceToRemove = null;
						break;
				}
				action = PopPending();
			}

			WorkerThread = null;
		}
	}
}
