using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

			if (request.ClientType == ConnectRequest.ClientTypes.Ship)
			{
				ConnectResponce responce = new ConnectResponce();
				responce.Responce = ConnectResponce.ResponceTypes.Accepted;
				responce.ScenarioName = ActiveScenario.in

				UpdateShipList shipList = new UpdateShipList();
				GetShipList(shipList);
				peer.Send(shipList);
			}
			else
			{

			}
		}
	}
}
