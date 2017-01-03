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
		private readonly INetworkPlayerEntityCollection playerEntityCollection;

		protected override void OnIncomingHandlableMessage(IRequestMessage message, PlayerSpawnRequestPayload payload, IMessageParameters parameters, InstanceClientSession peer)
		{
			//Important to check if we've actually already created an entity for this connection
			if(playerEntityCollection.ContainsKey(peer.PeerDetails.ConnectionID))
			{
				if (Logger.IsWarnEnabled)
					Logger.Warn($"Session with ID: {peer.PeerDetails.ConnectionID} of Type: {peer.GetType().Name} tried to spawn a player but a player has already been spawned for this session.");

				//TODO: Send error response

				//Don't continue. Something is broken, if in development, or if deployed a malicious actor
				return;
			}

			//rely on the factory implementation to handle placement details such as position and rotation
			IEntitySpawnDetails details = playerEntityFactory.SpawnPlayerEntity(peer.PeerDetails.ConnectionID);

			//Add the network tag
			//TODO: This is not really good design
			details.EntityGameObject.AddComponent<NetworkPeerEntityTag>().Initialize(peer); //initialize it with the peer context

			//TODO: Clean this up
			Vector3Surrogate pos = details.Position.ToSurrogate();

			QuaternionSurrogate rot = details.Rotation.ToSurrogate();

			//Send the response to the player who requested to spawn
			peer.SendResponse(new PlayerSpawnResponsePayload(PlayerSpawnResponseCode.Success, pos, rot, details.EntityId), DeliveryMethod.ReliableUnordered, true, 0);

			//broadcast the spawn to all other players and spawn every other player on the connecting peer.
			//TODO: Use broadcasting functionality whenever it's implemented
			foreach (INetPeer p in playerEntityCollection.AllPeers())
			{
				Logger.Debug("In Loop!");

				//Check that we don't send spawn event to the original requester
				if (p.PeerDetails.ConnectionID != peer.PeerDetails.ConnectionID)
				{
					//Send the player spawn event
					p.TrySendMessage(OperationType.Event, new PlayerSpawnEventPayload(details.EntityId, pos, rot, "NoneRightNow"), DeliveryMethod.ReliableUnordered, true, 0);

					//Send a spawn event of this peer/player too to the player who requested to spawn
					peer.SendEvent(new PlayerSpawnEventPayload(p.PeerDetails.ConnectionID, playerEntityCollection[p.PeerDetails.ConnectionID].transform.position.ToSurrogate(), playerEntityCollection[p.PeerDetails.ConnectionID].transform.rotation.ToSurrogate(), "No Name"),
						DeliveryMethod.ReliableUnordered, true, 0);
				}
			}
		}
	}
}
