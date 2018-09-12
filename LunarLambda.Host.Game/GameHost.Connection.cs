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
				GetShipList(shipList);
				peer.Send(shipList);
			}
		}
	}
}
