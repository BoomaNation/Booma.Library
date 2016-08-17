using GladNet.Engine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using GladNet.Common;
using GladNet.ASP.Client.Lib;
using SceneJect.Common;
using GladNet.ASP.Client.RestSharp;
using GladNet.Message;
using GladNet.Serializer;
using Common.Logging;
using GladNet.Message.Handlers;
using Easyception;
using GladNet.ASP.Client.RestSharp.Middleware.Authentication;

namespace Booma.Client.Network.Common
{
	[Injectee]
	public class BoomaNetworkWebPeer<TInheritingType> : MonoBehaviour, INetPeer, INetworkMessageReceiver
		where TInheritingType : BoomaNetworkWebPeer<TInheritingType>
	{
		[SerializeField]
		private string BaseUrl;

		//INetPeer
		public INetworkMessageRouterService NetworkSendService { get; private set; }

		public IConnectionDetails PeerDetails { get; private set; }

		public NetStatus Status { get; private set; }

		protected IWebRequestEnqueueStrategy enqueuStrat { get; private set; }

		[Inject]
		private readonly ISerializerStrategy serializer;

		[Inject]
		private readonly IDeserializerStrategy deserializer;

		[Inject]
		protected readonly ILog classLogger;

		/// <summary>
		/// Handler service that will deal with dispatching <see cref="ResponseMessage"/> payloads that are of 
		/// type <see cref="PacketPayload"/> and will generally have handling logic implemented/abstracted from this peer.
		/// </summary>
		[Inject]
		private IResponseMessageHandlerService<TInheritingType> responseHandler { get; set; } //we have to have a setter, can't use C#6 readonly prop because FasterFlect when using SceneJect will complain that it can't find a setter.

		//call on Start, not awake.
		public virtual void Start()
		{
			//Uses HTTPS so default to established encryption
			Status = NetStatus.EncryptionEstablished;

			enqueuStrat = new RestSharpCurrentThreadEnqueueRequestHandlerStrategy(BaseUrl, deserializer, serializer, this, 0, new DefaultNetworkMessageRouteBackService(new AUIDServiceCollection<INetPeer>(1), classLogger));

			NetworkSendService = new WebPeerClientMessageSender(enqueuStrat);

			PeerDetails = new WebClientPeerDetails(BaseUrl, 0, 0);
		}

		public bool CanSend(OperationType opType)
		{
			//Web peers can only send requests.
			return opType == OperationType.Request;
		}

		public void OnNetworkMessageReceive(IRequestMessage message, IMessageParameters parameters)
		{
			throw new NotImplementedException("Web peers cannot handle requests.");
		}

		public void OnNetworkMessageReceive(IResponseMessage message, IMessageParameters parameters)
		{
			Throw<ArgumentNullException>.If.IsNull(message)?.Now(nameof(message), $"Cannot have a null {nameof(IResponseMessage)} in on recieve. This should never occur internally. Indicates major fault. Should never reach this point.");
			responseHandler.TryProcessMessage(message, null, this as TInheritingType);
		}

		public void OnNetworkMessageReceive(IEventMessage message, IMessageParameters parameters)
		{
			throw new NotImplementedException("Web peers cannot handle events.");
		}

		public void OnNetworkMessageReceive(IStatusMessage status, IMessageParameters parameters)
		{
			throw new NotImplementedException("Web peers cannot handle status updates.");
		}
	}
}
