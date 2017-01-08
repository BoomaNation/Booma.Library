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
	//TODO: Generalize for all entities. Not just players.
	[Injectee]
	public class PlayerEntitySpawnEventHandler : EventPayloadHandlerComponent<InstanceClientPeer, EntitySpawnEventPayload>
	{
		[Inject]
		private readonly IPlayerEntityFactory playerFactory;

		[Inject]
		private readonly ILog logger;

		protected override void OnIncomingHandlableMessage(IEventMessage message, EntitySpawnEventPayload payload, IMessageParameters parameters, InstanceClientPeer peer)
		{
			logger.Info($"Recieved spawn event for ID: {payload.EntityGuid.EntityId}.");

			playerFactory.SpawnPlayerEntity(payload.Position.ToVector3(), payload.Rotation.ToQuaternion(), new NetworkPlayerSpawnContext(payload.EntityGuid, peer));
		}
	}
}
