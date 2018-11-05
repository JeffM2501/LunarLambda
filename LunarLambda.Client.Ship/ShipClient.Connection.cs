using System;
using LunarLambda.Data.Databases;
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

        public event EventHandler<JoinShipResponce> ShipJoinRejected = null;
        public event EventHandler<JoinShipResponce> JoinedShip = null;

        public ConnectResponce LastConnectResponce { get; protected set; } = null;
        public UpdateShipList LastShipList { get; protected set; } = null;

        public ShipTemplate ConnectedShip = null;

        public void HandleConnectResponce(object sender, ShipMessage message)
        {
            LastConnectResponce = message as ConnectResponce;

            ConnectedShip = null;

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

        public void JoinShipGame(UpdateShipList.ShipInfo ship, string password)
        {
            ShipRequest request = new ShipRequest();
            request.Join = true;
            request.RequestedShipID = ship.ID;
            request.Password = password;
            request.Name = ship.Name;

            Send(request);
        }

        public void CreateShip(UpdateShipList.ShipInfo ship, string name, string password)
        {
            ShipRequest request = new ShipRequest();
            request.Join = false;
            request.RequestedShipID = ship.ID;
            request.Password = password;
            request.Name = name;

            Send(request);
        }

        public void HandleJoinShipResponce(object sender, ShipMessage message)
        {
            JoinShipResponce responce = message as JoinShipResponce;

            if (responce.Error)
            {
                ConnectedShip = null;
                ShipJoinRejected?.Invoke(this, responce);
                return;
            }

            ConnectedShip = new ShipTemplate();
            ConnectedShip.ID = responce.ShipID;
            ConnectedShip.Name = responce.ShipName;
            ConnectedShip.ClassName = responce.ShipClass;

            JoinedShip?.Invoke(this, responce);
        }
    }
}
