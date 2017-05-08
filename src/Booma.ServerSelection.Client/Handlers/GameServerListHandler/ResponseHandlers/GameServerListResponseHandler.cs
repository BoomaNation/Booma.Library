using Booma.Client.Network.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Message;
using GladNet.Payload.Authentication;
using Booma.Payloads.ServerSelection;
using UnityEngine;
using SceneJect.Common;

namespace Booma.Client.ServerSelection.Authentication
{
	[Injectee]
	public class GameServerListResponseHandler : ResponsePayloadHandlerComponent<GameServerListWebClient, GameServerListResponsePayload>
	{
		[Inject]
		private readonly IGameServerDetailsGameObjectFactory detailsFactory;

		protected override void OnIncomingHandlableMessage(IResponseMessage message, GameServerListResponsePayload payload, IMessageParameters parameters, GameServerListWebClient peer)
		{
			Debug.Log($"Handling response game server list {payload.GameServerDetails.Count()}");

			//TODO: Error handling
			if(payload.ResponseCode == GameServerListResponseCode.Success)
				foreach(SimpleGameServerDetailsModel details in payload.GameServerDetails)
					detailsFactory.Create(details.Name, details.Region);
		}
	}
}
