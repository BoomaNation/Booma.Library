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
#pragma warning disable 649

namespace Booma.Instance.Client
{
	//TODO: Generalize for all entities. Not just players.
	[Injectee]
	public class EntityPositionUpdateEventHandler : EventPayloadHandlerComponent<InstanceClientPeer, EntityPositionUpdateEvent>
	{
		[Inject]
		private readonly ILog logger;

		[Inject]
		private readonly NetworkEntityCollection entityCollection;

		protected override void OnIncomingHandlableMessage(IEventMessage message, EntityPositionUpdateEvent payload, IMessageParameters parameters, InstanceClientPeer peer)
		{
			//logger.Info($"Recieved position event for ID: {payload.EntityGuid.EntityId}.");

			if (entityCollection.ContainsKey(payload.EntityGuid))
			{
				entityCollection[payload.EntityGuid].WorldObject.transform.position = payload.Position.ToVector3();
			}
			else
				Debug.Log($"Recieved position update event from Type: {payload.EntityGuid.EntityType} Id: {payload.EntityGuid.EntityId} but we've not spawned an entity with that GUID.");
		}
	}
}
