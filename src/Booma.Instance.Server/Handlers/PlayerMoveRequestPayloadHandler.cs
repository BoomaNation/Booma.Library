using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Message;
using SceneJect.Common;
using Booma;
using UnityEngine;

namespace Booma
{
	[Injectee]
	public class PlayerMoveRequestPayloadHandler : RequestPayloadHandlerComponent<InstanceClientSession, PlayerMoveRequestPayload>
	{
		[Inject]
		private IConnectionToGuidLookupService GuidLookupService { get; }

		[Inject]
		private NetworkEntityCollection EntityCollection { get; }

		protected override void OnIncomingHandlableMessage(IRequestMessage message, PlayerMoveRequestPayload payload, IMessageParameters parameters, InstanceClientSession peer)
		{
			NetworkEntityGuid guid = GuidLookupService.Lookup(peer.PeerDetails.ConnectionID);

			if (guid == null)
				throw new InvalidOperationException($"Couldn't find GUID for Connection ID: {peer.PeerDetails.ConnectionID}.");

			EntityCollection[guid].WorldObject.transform.position = payload.Position.ToVector3();
		}
	}
}
