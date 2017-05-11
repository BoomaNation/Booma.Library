using Booma.Client.Network.Common;
using Booma.Payloads.ServerSelection;
using GladNet.ASP.Client.RestSharp;
using GladNet.ASP.Client.RestSharp.Middleware.Authentication;
using GladNet.Message;
using GladNet.Payload;
using GladNet.Serializer;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Client.ServerSelection
{
	/// <summary>
	/// Peer component that will communicate with the gameserver list service (ship list service)
	/// </summary>
	public class GameServerListWebClient : BoomaNetworkWebPeer<GameServerListWebClient>
	{
		/// <inheritdoc />
		protected override void RegisterPayloads(ISerializerRegistry registerService)
		{
			registerService.RegisterServerSelectionPayloads();
		}
	}
}
