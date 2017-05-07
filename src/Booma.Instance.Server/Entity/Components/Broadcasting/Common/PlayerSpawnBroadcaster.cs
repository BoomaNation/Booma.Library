using Booma.Entity.Identity;
using Booma.Instance.Common;
using Booma.Payloads.Instance;
using GladNet.Common;
using GladNet.Engine.Common;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Booma.Instance.Server
{
	[Injectee]
	public class PlayerSpawnBroadcaster : NetworkMessageBroadcaster
	{
		[SerializeField]
		private Transform entityTransform;

		[SerializeField]
		private IEntityGuidContainer guidContainer;

		[Inject]
		private readonly NetworkEntityCollection entityCollection;

		[Inject]
		private readonly INetPeer peer;

		protected override void Start()
		{
			base.Start();

			//Broadcast spawn on start
			this.messageBroadcaster.SendEvent(new PlayerEntitySpawnEventPayload(guidContainer.NetworkGuid, entityTransform.position.ToSurrogate(), entityTransform.rotation.ToSurrogate()),
				DeliveryMethod.ReliableUnordered, false, 0);

			//After broadcasting this entities spawn we should send spawn events for each other connected player.
			foreach(var entity in entityCollection.Values.Where(e => e.NetworkGuid.EntityType == EntityType.Player))
			{
				//Don't send spawn event about self to self
				if(entity.NetworkGuid != guidContainer.NetworkGuid)
					peer.TrySendMessage(OperationType.Event, new PlayerEntitySpawnEventPayload(entity.NetworkGuid, entity.WorldObject.transform.position.ToSurrogate(), entity.WorldObject.transform.rotation.ToSurrogate()),
						DeliveryMethod.ReliableUnordered, false, 0);
			}
		}
	}
}
