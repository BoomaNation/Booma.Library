using Booma.Client.Network.Common;
using Booma.Payloads.Instance;
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
	public class PlayerEntitySpawnEventHandler : EventPayloadHandlerComponent<InstanceClientPeer, PlayerEntitySpawnEventPayload>
	{
		[Inject]
		private readonly IPlayerEntityFactory playerFactory;

		[Inject]
		private readonly ILog logger;

		[Inject]
		private readonly NetworkEntityCollection entityCollection;

		protected override void OnIncomingHandlableMessage(IEventMessage message, PlayerEntitySpawnEventPayload payload, IMessageParameters parameters, InstanceClientPeer peer)
		{
			logger.Info($"Recieved spawn event for ID: {payload.EntityGuid.EntityId}.");

			IEntitySpawnResults details = playerFactory.SpawnPlayerEntity(payload.Position.ToVector3(), payload.Rotation.ToQuaternion(), new NetworkPlayerSpawnContext(payload.EntityGuid, peer));

			if (details.Result != SpawnResult.Success)
				throw new InvalidOperationException($"Failed to spawn entity with Type: {payload.EntityGuid.EntityType} Id: {payload.EntityGuid.EntityId}.");

			//TODO: Should we do this in the factory?
			//Add to collection
			entityCollection.Add(payload.EntityGuid, new EntityContainer(payload.EntityGuid, details.EntityGameObject));

			OnCreatedEntity(details.EntityGameObject);
		}

		protected virtual void OnCreatedEntity(GameObject entity)
		{
			//We don't need to do anything. Consumers may want to do more to this.
		}
	}
}
