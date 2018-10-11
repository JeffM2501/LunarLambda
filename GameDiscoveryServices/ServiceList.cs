using System;
using System.Collections.Generic;

namespace GameDiscoveryServices
{
    public static class ServiceList
	{
		public static event EventHandler ServiceListUpdated = null;

		public static List<HostedService> KnownServices = new List<HostedService>();
        public static HostedService LocalService = null;

		public static void RegisterLocalService(HostedService localService, bool publish)
		{
			localService.IsLocal = true;

            if (LocalService != null && LocalService.WasPublished)
                InternetDiscoveryConnection.PushPublicSevice(LocalService);

            LocalService = localService;

			if (publish)
				InternetDiscoveryConnection.PushPublicSevice(localService);

            CallUpdateNotifiation();
        }

		public static void RemoveLocalService()
		{
			if (LocalService != null && LocalService.WasPublished)
				InternetDiscoveryConnection.PushPublicSevice(LocalService);

            LocalService = null;

            CallUpdateNotifiation();

        }

		internal static void AddRemoteService(HostedService service)
		{
            if (service.IDKey == LocalService.IDKey)
                return;

            HostedService existing = KnownServices.Find((x) => x.IDKey == service.IDKey);
			if (existing != null)
				KnownServices.Remove(existing);

			KnownServices.Add(service);
		}

		internal static void CallUpdateNotifiation()
		{
			ServiceListUpdated?.Invoke(null, EventArgs.Empty);
		}
	}
}
