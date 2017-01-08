using Booma.Payloads.Instance;
using Booma.Server.Network.Unity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Message;
using SceneJect.Common;
using Booma.Instance.Common;
using GladNet.Engine.Common;
using Booma.Payloads.Surrogates.Unity;
using GladNet.Common;
using UnityEngine;
using Booma.Entity.Identity;

namespace Booma.Instance.Server
{
	[Injectee]
	public class PlayerSpawnRequestPayloadHandler : RequestPayloadHandlerComponent<InstanceClientSession, PlayerSpawnRequestPayload>
	{
		/// <summary>
		/// Factory service for the player entities.
		/// </summary>
		[Inject]
		private readonly IPlayerEntityFactory playerEntityFactory;

		/// <summary>
		/// Player entity collection.
		/// </summary>
		[Inject]
		private readonly NetworkEntityCollection entityCollection;

		//TODO: This is temporary. We don't need this service once full handshake is implemented. For demo we need it though
		[Inject]
		private readonly INetworkGuidFactory entityGuidFactory;

		protected override void OnIncomingHandlableMessage(IRequestMessage message, PlayerSpawnRequestPayload payload, IMessageParameters parameters, InstanceClientSession peer)
		{
			//TODO: Right now we don't have a full handshake for entering an instance. So we need to make up a GUID for the entering player
			//Important to check if we've actually already created an entity for this connection
			//We don't have that implemenented though for the demo

			//rely on the factory implementation to handle placement details such as position and rotation
			NetworkEntityGuid guid = entityGuidFactory.Create(EntityType.Player);
			IEntitySpawnResults details = playerEntityFactory.SpawnPlayerEntity(new NetworkPlayerSpawnContext(guid, peer));

			if (details.Result != SpawnResult.Success)
				throw new InvalidOperationException($"Failed to create Entity for {peer.ToString()}. Failure: {details.Result.ToString()}");

			entityCollection.Add(guid, details.EntityGameObject);

			//TODO: Clean this up
			Vector3Surrogate pos = details.EntityGameObject.transform.rotation.ToSurrogate();

			QuaternionSurrogate rot = details.EntityGameObject.transform.rotation.ToSurrogate();

			//Send the response to the player who requested to spawn
			peer.SendResponse(new PlayerSpawnResponsePayload(PlayerSpawnResponseCode.Success, pos, rot, guid), DeliveryMethod.ReliableUnordered, true, 0);
		}
	}
}
