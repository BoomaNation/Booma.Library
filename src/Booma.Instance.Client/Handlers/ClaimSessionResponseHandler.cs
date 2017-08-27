using Booma.Payloads.Instance;
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
	public class ClaimSessionResponseHandler : ResponsePayloadHandlerComponent<InstanceClientPeer, ClaimSessionResponsePayload>
	{
		//TODO: Hide this behind provider
		[SerializeField]
		private GameObject playerPrefab;

		[Inject]
		private readonly IGameObjectFactory gameobjectFactory;

		[Inject]
		private readonly NetworkEntityCollection entityCollection;

		protected override void OnIncomingHandlableMessage(IResponseMessage message, ClaimSessionResponsePayload payload, IMessageParameters parameters, InstanceClientPeer peer)
		{
			//TODO: Implement spawning
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved player spawn response. {payload.ResponseCode} {payload.Position} {payload.Rotation}");

			GameObject entityGameObject = gameobjectFactory.CreateBuilder()
				.With(Service<INetPeer>.As(peer))
				.With(Service<NetworkEntityGuid>.As(payload.EntityGuid))
				.Create(playerPrefab, payload.Position.ToVector3(), payload.Rotation.ToQuaternion());

			//TODO: Should we do this in the factory?
			//Add to collection
			entityCollection.Add(payload.EntityGuid, new EntityContainer(payload.EntityGuid, entityGameObject));

			OnCreatedEntity(entityGameObject);
		}

		protected virtual void OnCreatedEntity(GameObject entity)
		{
			//We don't need to do anything. Consumers may want to do more to this.
		}
	}
}
