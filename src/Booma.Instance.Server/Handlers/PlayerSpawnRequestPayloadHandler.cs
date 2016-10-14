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
		private readonly IServerPlayerEntityCollection playerEntityCollection;

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

			foreach (INetPeer p in playerEntityCollection.AllPeers())
			{
				Vector3Surrogate pos = new Vector3Surrogate(details.Position.x, details.Position.y, details.Position.z);

				QuaternionSurrogate rot = new QuaternionSurrogate(details.Rotation.x, details.Rotation.y, details.Rotation.z, details.Rotation.w);

				//Check that we don't send spawn event to the original requester
				if (p.PeerDetails.ConnectionID != peer.PeerDetails.ConnectionID)
					//Send the player spawn event
					p.TrySendMessage(OperationType.Event, new PlayerSpawnEventPayload(details.EntityId, pos, rot, "NoneRightNow"), DeliveryMethod.ReliableUnordered, true, 0);
				else
					//Send the response to the player who requested to spawn
					p.TrySendMessage(OperationType.Response, new PlayerSpawnResponsePayload(PlayerSpawnResponseCode.Success, pos, rot), DeliveryMethod.ReliableUnordered, true, 0);
			}
		}
	}
}
