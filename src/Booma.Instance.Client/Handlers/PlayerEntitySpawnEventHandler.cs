using Booma.Client.Network.Common;
using Booma.Payloads.Instance;
using GladNet.Message.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Message;
using Booma.Instance.Common;
using SceneJect.Common;
using Common.Logging;
using UnityEngine;

namespace Booma.Instance.Client
{
	[Injectee]
	public class PlayerEntitySpawnEventHandler : EventPayloadHandlerComponent<InstanceClientPeer, PlayerSpawnEventPayload>
	{
		[Inject]
		private readonly IPlayerEntityFactory playerFactory;

		[Inject]
		private readonly ILog logger;

		protected override void OnIncomingHandlableMessage(IEventMessage message, PlayerSpawnEventPayload payload, IMessageParameters parameters, InstanceClientPeer peer)
		{
			//TODO: Implement spawning
			logger.Info($"Recieved spawn event for ID: {payload.EntityId}.");

			playerFactory.SpawnPlayerEntity(payload.EntityId, new Vector3(payload.Position.X, payload.Position.Y, payload.Position.Z), new Quaternion(payload.Rotation.X, payload.Rotation.Y, payload.Rotation.Z, payload.Rotation.W));
		}
	}
}
