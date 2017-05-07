using GladBehaviour.Common;
using GladNet.Engine.Common;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Common;
using GladNet.Message;
using GladNet.Payload;

namespace Booma.Instance.Client
{
	[Injectee]
	public abstract class NetworkMessageSender : GladMonoBehaviour
	{
		[Inject]
		private readonly INetPeer peer;

		protected SendResult SendRequest(PacketPayload payload, DeliveryMethod deliveryMethod, bool encrypt = false, byte channel = 0)
		{
			return peer.NetworkSendService.TrySendMessage(OperationType.Request, payload, deliveryMethod, encrypt, channel);
		}

		protected SendResult SendRequest<TPacketType>(TPacketType payload) 
			where TPacketType : PacketPayload, IStaticPayloadParameters
		{
			return SendRequest(payload, payload.DeliveryMethod, payload.Encrypted, payload.Channel);
		}
	}
}
