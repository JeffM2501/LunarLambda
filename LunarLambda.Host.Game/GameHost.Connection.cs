
using LunarLambda.Data.Databases;
using LunarLambda.Data.Entitites;
using LunarLambda.Data.Zones;
using LunarLambda.Messges.Ship;
using LunarLambda.Messges.Ship.Connect;
using LunarLambda.Messges.Ship.Game;

namespace LunarLambda.Host.Game
{
    public partial class GameHost
	{
		private void HandleConnectRequest(object sender, ShipMessage msg)
		{
			ShipPeer peer = sender as ShipPeer;
			ConnectRequest request = msg as ConnectRequest;

			ConnectResponce responce = new ConnectResponce();
			responce.Responce = ConnectResponce.ResponceTypes.Accepted;
			responce.ScenarioName = StartupInfo.SelectedScenario.Name;
			responce.ScenarioAuthor = StartupInfo.SelectedScenario.Author;
			responce.ScenarioDescription = StartupInfo.SelectedScenario.Description;
			responce.ScenarioType = StartupInfo.SelectedScenario.Type;
			responce.ScenarioVariation = string.Empty;
			if (StartupInfo.SelectedVariation != null)
				responce.ScenarioVariation = StartupInfo.SelectedVariation.DisplayName;

			responce.ScenarioIconImage = StartupInfo.SelectedScenario.IconImage;

			responce.ServerKey = ServiceInfo.IDKey;
			responce.ServerLANAddress = StartupInfo.ServerLANAddress;
			responce.SerververWANAddress = StartupInfo.ServerWANHost;
			peer.Send(responce);

			if (request.ClientType == ConnectRequest.ClientTypes.Ship)
			{
                UpdateShipList shipList = new UpdateShipList();
				GetPlayableShipList(shipList);
				peer.Send(shipList);
			}
		}

        private void HandleShipRequest(object sender, ShipMessage msg)
        {
            ShipPeer peer = sender as ShipPeer;
            ShipRequest request = msg as ShipRequest;

            JoinShipResponce responce = new JoinShipResponce();

            PlayerShip shipToLink = null;

            if (peer.LinkedShip != null)
            {
                shipToLink = peer.LinkedShip;
            }
            else
            {
                if (request.Join)
                {
                    shipToLink = PlayerShips.Find((x) => x.LinkedShip.GUID == request.RequestedShipID);
                    if (shipToLink == null || shipToLink.Locked && shipToLink.Password != request.Password)
                    {
                        responce.Error = true;
                        responce.Message = "NoShipToJoin";
                        peer.Send(responce);
                        return; // nope!
                    }
                    else
                    {
                        if (!shipToLink.ControlingPeers.Contains(peer))
                            shipToLink.ControlingPeers.Add(peer);

                        peer.LinkedShip = shipToLink;
                    }
                }
            }

            if (shipToLink == null)
            {
                var template = TemplateDatabase.GetTemplate(request.RequestedShipID);
                int shipID = ActiveScenario.SpawnPlayableShip(peer.Connection.RemoteUniqueIdentifier.ToString(), template == null ? string.Empty : template.Name, request.Name);

                var ship = ZoneManager.GetFirstMatch(new PlayableShipFinder(shipID)) as Ship;

                shipToLink = new PlayerShip();
                shipToLink.LinkedShip = ship;
                shipToLink.ControlingPeers.Add(peer);
                shipToLink.Locked = request.Password != string.Empty;
                shipToLink.Password = request.Password;

                peer.LinkedShip = shipToLink;
                PlayerShips.Add(shipToLink);
            }

            // send back the responce with the ship they are on
            responce.Error = false;
            responce.Message = shipToLink.LinkedShip.Name;
            responce.ShipID = shipToLink.LinkedShip.GUID;
            peer.Send(responce);

            // send an info and status update
        }
    }
}
