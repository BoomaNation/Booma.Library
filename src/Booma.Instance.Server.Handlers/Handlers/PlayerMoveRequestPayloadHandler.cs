using Booma.Payloads.Instance;
using Booma.Server.Network.Unity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Message;
using SceneJect.Common;
using Booma.Entity.Identity;
using Booma.Instance.Common;
using UnityEngine;

namespace Booma.Instance.Server
{
	[Injectee]
	public class PlayerMoveRequestPayloadHandler : RequestPayloadHandlerComponent<InstanceClientSession, PlayerMoveRequestPayload>
	{
		[Inject]
		private readonly IConnectionToGuidLookupService guidLookupService;

		[Inject]
		private readonly NetworkEntityCollection entityCollection;

		protected override void OnIncomingHandlableMessage(IRequestMessage message, PlayerMoveRequestPayload payload, IMessageParameters parameters, InstanceClientSession peer)
		{
			NetworkEntityGuid guid = guidLookupService.Lookup(peer.PeerDetails.ConnectionID);

			if (guid == null)
				throw new InvalidOperationException($"Couldn't find GUID for Connection ID: {peer.PeerDetails.ConnectionID}.");

			entityCollection[guid].WorldObject.transform.position = payload.Position.ToVector3();
		}
	}
}
