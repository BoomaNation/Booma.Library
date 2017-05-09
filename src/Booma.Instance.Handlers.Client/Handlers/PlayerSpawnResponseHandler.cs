using Booma.Payloads.Instance;
using GladNet.Message.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booma.Client.Network.Common;
using GladNet.Message;
using Booma.Instance.Common;
using SceneJect.Common;
using Common.Logging;
using UnityEngine;
using GladNet.Engine.Common;
using Booma.Entity.Identity;

namespace Booma.Instance.Client
{
	[Injectee]
	public class PlayerSpawnResponseHandler : ResponsePayloadHandlerComponent<InstanceClientPeer, PlayerSpawnResponsePayload>
	{
		[Inject]
		private readonly ILog logger;

		//TODO: Hide this behind provider
		[SerializeField]
		private GameObject playerPrefab;

		[Inject]
		private readonly IGameObjectFactory gameobjectFactory;

		protected override void OnIncomingHandlableMessage(IResponseMessage message, PlayerSpawnResponsePayload payload, IMessageParameters parameters, InstanceClientPeer peer)
		{
			//TODO: Implement spawning
			logger.Info($"Recieved player spawn response. {payload.ResponseCode} {payload.Position} {payload.Rotation}");

			gameobjectFactory.CreateBuilder()
				.With(Service<INetPeer>.As(peer))
				.With(Service<NetworkEntityGuid>.As(payload.EntityGuid))
				.Create(playerPrefab, payload.Position.ToVector3(), payload.Rotation.ToQuaternion());
		}
	}
}
