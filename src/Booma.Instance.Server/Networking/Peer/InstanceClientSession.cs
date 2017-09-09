using GladNet.Engine.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using GladNet.Engine.Common;
using GladNet.Message;
using GladNet.Message.Handlers;

namespace Booma
{
	public class InstanceClientSession : ClientPeerSession
	{
		/// <summary>
		/// <see cref="IRequestMessage"/> handling service for the <see cref="InstanceClientSession"/>.
		/// </summary>
		private IRequestMessageHandlerService<InstanceClientSession> requestMessageHandlerService { get; }

		public InstanceClientSession(ILog logger, INetworkMessagePayloadSenderService sender, IConnectionDetails details, 
			INetworkMessageSubscriptionService subService, IDisconnectionServiceHandler disconnectHandler, IRequestMessageHandlerService<InstanceClientSession> requestMessageHandlers) 
			: base(logger, sender, details, subService, disconnectHandler)
		{
			if (requestMessageHandlers == null)
				throw new ArgumentNullException(nameof(requestMessageHandlers), $"Cannot provide a null {nameof(IRequestMessageHandlerService<InstanceClientSession>)}.");

			requestMessageHandlerService = requestMessageHandlers;
		}

		protected override void OnReceiveRequest(IRequestMessage requestMessage, IMessageParameters parameters)
		{
			if (requestMessage == null)
				throw new ArgumentNullException(nameof(requestMessage), $"GladNet produced a null {nameof(IRequestMessage)}");

			bool handlingResult = requestMessageHandlerService.TryProcessMessage(requestMessage, parameters, this);

			//if it wasn't handable log a warning about it.
			//Malicious clients could send something like this. It'd likely be caught higher up the stack though.
			if (!handlingResult)
				Logger.Warn($"Client Session ID: {PeerDetails.ConnectionID} recieved a request with a payload that cannot be handled. Payload Type: {requestMessage.Payload?.Data?.GetType()?.Name}");
		}
	}
}
