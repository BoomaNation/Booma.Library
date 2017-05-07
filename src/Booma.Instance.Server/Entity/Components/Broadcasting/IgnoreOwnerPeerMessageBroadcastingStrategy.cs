using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Common;
using GladNet.Message;
using GladNet.Payload;
using UnityEngine;
using GladNet.Engine.Common;
using SceneJect.Common;
using Booma.Entity.Identity;
using Booma.Instance.Common;

namespace Booma.Instance.Server
{
	[Injectee]
	public class IgnoreOwnerPeerMessageBroadcastingStrategy : MonoBehaviour, IMessageBroadcastingService
	{
		[Inject]
		private readonly IReadonlyPeerCollection peers;

		[Inject]
		public readonly INetworkOwnable peerToIgnore;

		public SendResult SendEvent(PacketPayload payload, DeliveryMethod deliveryMethod, bool encrypt = false, byte channel = 0)
		{
			//TODO: Get true broadcasting functionality
			foreach (INetPeer p in peers)
				if(peerToIgnore.OwnerPeer.PeerDetails.ConnectionID != p.PeerDetails.ConnectionID) //ignore the peer with the ID.
					p.TrySendMessage(OperationType.Event, payload, deliveryMethod, encrypt, channel);

			//This is a 1 to N broadcast. Can't really respond with full details using a SendResult
			return SendResult.Sent;
		}

		public SendResult SendEvent<TPacketType>(TPacketType payload)
			where TPacketType : PacketPayload, IStaticPayloadParameters
		{
			return SendEvent(payload, payload.DeliveryMethod, payload.Encrypted, payload.Channel);
		}
	}
}
