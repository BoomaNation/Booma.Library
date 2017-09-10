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
		private NetworkEntityCollection EntityCollection { get; }

		[Inject]
		private IConnectionToGuidLookupService GuidLookupService { get; }

		protected override void OnIncomingHandlableMessage(IRequestMessage message, EntityInteractionRequestPayload payload, IMessageParameters parameters, InstanceClientSession peer)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Recieved interaction request for {payload.NetworkGuid.EntityId}.");

			if (EntityCollection.ContainsKey(payload.NetworkGuid))
			{
				IWorldInteractable interactable = EntityCollection[payload.NetworkGuid]?.WorldObject?.GetComponent<IWorldInteractable>();

				if(interactable == null)
				{
					if(Logger.IsWarnEnabled)
						Logger.Warn($"Recieved interaction request for entity that can't be interacted with ID: {payload.NetworkGuid.EntityId} Type: {payload.NetworkGuid.EntityType} from NetId: {peer.PeerDetails.ConnectionID}.");
				}
				else
				{
					interactable.TryInteract(GuidLookupService.Lookup(peer.PeerDetails.ConnectionID));
				}
			}
			else
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Recieved interaction request for unknown entity ID: {payload.NetworkGuid.EntityId} Type: {payload.NetworkGuid.EntityType} from NetId: {peer.PeerDetails.ConnectionID}.");
		}
	}
}
