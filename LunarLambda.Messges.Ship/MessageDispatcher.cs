using System;
using System.Collections.Generic;

namespace LunarLambda.Messges.Ship
{
    public class MessageDispatcher
    {
        protected class DispatchData
        {
            public Type MessageType = null;
            public event EventHandler<ShipMessage> Handlers  = null;

            public DispatchData(Type t)
            {
                MessageType = t;
            }

            public void Dispatch(object sender, ShipMessage message)
            {
                Handlers?.Invoke(sender, message);
            }
        }

        protected Dictionary<Type, DispatchData> MessageEvents = new Dictionary<Type, DispatchData>();

        public void RegisterHandler(Type t, EventHandler<ShipMessage> callback)
        {
            if (!MessageEvents.ContainsKey(t))
                MessageEvents.Add(t, new DispatchData(t));

            MessageEvents[t].Handlers += callback;
        }

        public bool Dispatch(object sender, ShipMessage message)
        {
            if (message == null)
                return false;

            Type t = message.GetType();
            if (!MessageEvents.ContainsKey(t))
                return false;

            MessageEvents[t].Dispatch(sender, message);
            return true;
        }
    }
}
