using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Message;
using Common.Logging;
using Booma;
using UnityEngine;

namespace Booma
{

	[Injectee]
	public class GameObjectEntitySpawnEventHandler : EventPayloadHandlerComponent<InstanceClientPeer, GameObjectEntitySpawnEventPayload>
	{
		[Inject]
		private readonly ILog logger;

		[Inject]
		private readonly NetworkEntityCollection entityCollection;

		//[Inject]
		//private readonly IGameObjectPrefabEntityFactory objectFactory;

		protected override void OnIncomingHandlableMessage(IEventMessage message, GameObjectEntitySpawnEventPayload payload, IMessageParameters parameters, InstanceClientPeer peer)
		{
			/*logger.Debug($"Recieved GameObject spawn event with Id: {payload.EntityGuid.EntityId} and Type: {payload.PrefabId}.");

			IEntitySpawnResults details = objectFactory.TrySpawnEntity(payload.Position.ToVector3(), payload.Rotation.ToQuaternion(),
				payload.Scale.ToVector3(), new NetworkGameObjectPrefabSpawnContext(payload.EntityGuid, payload.PrefabId, peer));

			if (details.Result != SpawnResult.Success)
			{
				Debug.LogError($"Failed to create GameObject for Entity Id: {payload.EntityGuid.EntityId} Type: {payload.PrefabId}.");
				return;
			}

			entityCollection.Add(payload.EntityGuid, new EntityContainer(payload.EntityGuid, details.EntityGameObject));

			//TODO: Set default state.
			IEntityStateContainer state = details.EntityGameObject.GetComponentInChildren<IEntityStateContainer>();

			if (state == null)
				return;

			state.State = payload.CurrentState;*/
		}
	}
}
