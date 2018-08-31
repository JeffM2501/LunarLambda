using System;
using System.Collections.Generic;
using System.Reflection;

namespace LunarLambda.Messges.Ship
{
	public static class MessageManager
	{
		internal static Dictionary<string, Type> MessageTypes = new Dictionary<string, Type>();

		static MessageManager()
		{
			Init();
		}

		public static void Init()
		{
			if (MessageTypes.Count != 0)
				return;

			AddMessages(typeof(ShipMessage).Assembly);
		}

		public static void AddMessages(Assembly assembly)
		{
			foreach (var t in assembly.GetExportedTypes())
			{
				if (t.IsSubclassOf(typeof (ShipMessage)))
				{
					if (!MessageTypes.ContainsKey(t.Name))
						MessageTypes.Add(t.Name,t);
				}
			}
		}

		public static Type GetMessageType(string name)
		{
			if (MessageTypes.ContainsKey(name))
				return MessageTypes[name];

			return null;
		}
	}
}
