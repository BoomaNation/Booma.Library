using Booma.Payloads.Instance;
using Booma.Server.Network.Unity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Message;
using SceneJect.Common;
using Booma.Instance.Common;

namespace Booma.Instance.Server
{
	public class PlayerSpawnRequestPayloadHandler : RequestPayloadHandlerComponent<InstanceClientSession, PlayerSpawnRequestPayload>
	{
		[Inject]
		private readonly IPlayerEntityFactory playerEntityFactory;

		[Inject]
		private readonly IPlayerEntityCollection playerEntityCollection;

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
			playerEntityFactory.CreatePlayerEntity(peer.PeerDetails.ConnectionID);

			//TODO: Broadcast the new player to all clients
		}
	}
}
