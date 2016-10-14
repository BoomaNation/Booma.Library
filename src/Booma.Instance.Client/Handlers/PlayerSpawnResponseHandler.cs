using Booma.Client.Network.Common;
using Booma.Payloads.Instance;
using GladNet.Message.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Message;
using Booma.Instance.Common;
using SceneJect.Common;
using Common.Logging;

namespace Booma.Instance.Client
{
	[Injectee]
	public class PlayerSpawnResponseHandler : ResponsePayloadHandlerComponent<InstanceClientPeer, PlayerSpawnResponsePayload>
	{
		[Inject]
		private readonly ILog logger;

		protected override void OnIncomingHandlableMessage(IResponseMessage message, PlayerSpawnResponsePayload payload, IMessageParameters parameters, InstanceClientPeer peer)
		{
			//TODO: Implement spawning
			logger.Info($"Recieved player spawn response. {payload.ResponseCode} {payload.Position} {payload.Rotation}");
		}
	}
}
