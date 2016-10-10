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

namespace Booma.Instance.Server
{
	public class PlayerSpawnRequestPayloadHandler : RequestPayloadHandlerComponent<InstanceClientSession, PlayerSpawnRequestPayload>
	{
		[Inject]
		private readonly IPlayerEntityFactory playerEntityFactory;

		[Inject]
		private readonly IServerPlayerEntityCollection playerEntityCollection;

		protected override void OnIncomingHandlableMessage(IRequestMessage message, PlayerSpawnRequestPayload payload, IMessageParameters parameters, InstanceClientSession peer)
		{
			//Important to check if we've actually already created an entity for this connection
			if(playerEntityCollection.ContainsKey(peer.PeerDetails.ConnectionID))
			{
				if (Logger.IsWarnEnabled)
					Logger.Warn($"Session with ID: {peer.PeerDetails.ConnectionID} of Type: {peer.GetType().Name} tried to spawn a player but a player has already been spawned for this session.");

				//Don't continue. Something is broken, if in development, or if deployed a malicious broomop
				return;
			}

			//rely on the factory implementation to handle placement details such as position and rotation
			IEntitySpawnDetails details = playerEntityFactory.SpawnPlayerEntity(peer.PeerDetails.ConnectionID);

			foreach (INetPeer p in playerEntityCollection.AllPeers())
			{
				//Check that we don't send spawn event to the original requester
				if (p.PeerDetails.ConnectionID != peer.PeerDetails.ConnectionID)
					//Send the player spawn event
					p.TrySendMessage(GladNet.Common.OperationType.Event, new PlayerSpawnEventPayload(details.EntityId, new Vector3Surrogate(details.Position.x, details.Position.y, details.Position.z),
						new QuaternionSurrogate(details.Rotation.x, details.Rotation.y, details.Rotation.z, details.Rotation.w), "NoneRightNow"), GladNet.Common.DeliveryMethod.ReliableUnordered, true, 0);
			}

			//TODO: Tell the player that requested that they can spawn
		}
	}
}
