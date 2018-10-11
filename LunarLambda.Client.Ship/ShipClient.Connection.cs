using System;

using LunarLambda.Messges.Ship;
using LunarLambda.Messges.Ship.Connect;
using LunarLambda.Messges.Ship.Game;

namespace LunarLambda.Client.Ship
{
    public partial class ShipClient
    {
        public event EventHandler<ConnectResponce> ConnectionRejected = null;
        public event EventHandler<ConnectResponce> ConnectionAccepted = null;

        public event EventHandler<UpdateShipList> ShipListUpdated = null;

        public ConnectResponce LastConnectResponce { get; protected set; } = null;
        public UpdateShipList LastShipList { get; protected set; } = null;

        public void HandleConnectResponce(object sender, ShipMessage message)
        {
            LastConnectResponce = message as ConnectResponce;

            if (LastConnectResponce.Responce == ConnectResponce.ResponceTypes.Rejected)
                ConnectionRejected?.Invoke(this, LastConnectResponce);
            else
                ConnectionAccepted?.Invoke(this, LastConnectResponce);
        }

        public void HandleUpdateShipList (object sender, ShipMessage message)
        {
            LastShipList = message as UpdateShipList;
            ShipListUpdated?.Invoke(this, LastShipList);
        }
    }
}
