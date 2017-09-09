using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Message;
using Common.Logging;
using Booma;

namespace Booma
{
	[Injectee]
	public class EntityInteractionRequestPayloadHandler : RequestPayloadHandlerComponent<InstanceClientSession, EntityInteractionRequestPayload>
	{
		[Inject]
		private readonly ILog logger;

		[Inject]
		private readonly NetworkEntityCollection entityCollection;

		[Inject]
		private readonly IConnectionToGuidLookupService guidLookupService;

		protected override void OnIncomingHandlableMessage(IRequestMessage message, EntityInteractionRequestPayload payload, IMessageParameters parameters, InstanceClientSession peer)
		{
			logger.Debug($"Recieved interaction request for {payload.NetworkGuid.EntityId}.");

			if (entityCollection.ContainsKey(payload.NetworkGuid))
			{
				IWorldInteractable interactable = entityCollection[payload.NetworkGuid]?.WorldObject?.GetComponent<IWorldInteractable>();

				if(interactable == null)
					logger.Warn($"Recieved interaction request for entity that can't be interacted with ID: {payload.NetworkGuid.EntityId} Type: {payload.NetworkGuid.EntityType} from NetId: {peer.PeerDetails.ConnectionID}.");
				else
				{
					interactable.TryInteract(guidLookupService.Lookup(peer.PeerDetails.ConnectionID));
				}
			}
			else
				logger.Warn($"Recieved interaction request for unknown entity ID: {payload.NetworkGuid.EntityId} Type: {payload.NetworkGuid.EntityType} from NetId: {peer.PeerDetails.ConnectionID}.");
		}
	}
}
