using Booma.Server.Network.Unity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Engine.Common;
using GladNet.Engine.Server;
using GladNet.Serializer;
using GladNet.Message;
using Booma.Payloads.Instance;
using SceneJect.Common;
using GladNet.Message.Handlers;
using UnityEngine;

namespace Booma.Instance.Server
{
	/// <summary>
	/// Server-base for the Booma instance server.
	/// </summary>
	[Injectee]
	public class BoomaInstanceServerApplicationBase : BoomaServerApplicationBase
	{
		[Inject]
		private readonly IRequestMessageHandlerService<InstanceClientSession> instanceSessionRequestMessageHandlerService; //we use readonly and not C#6 get for sceneject

		/// <summary>
		/// UnityEvent invoked when a new session is created.
		/// </summary>
		[SerializeField]
		private OnNewSessionCreatedEvent OnNewSessionCreated;

		/// <summary>
		/// UnityEvent invoked when a session disconnects.
		/// </summary>
		[SerializeField]
		private OnSessionDisconnectedEvent OnSessionDisconnected;

		//For testing
		public void Update()
		{
			Poll();
		}

		/// <summary>
		/// Called internally by GladNet.
		/// </summary>
		/// <param name="registry">The serialization registry.</param>
		public override void RegisterPayloadTypes(ISerializerRegistry registry)
		{
			//Register the payloads.
			registry.Register(typeof(NetworkMessage));
			registry.Register(typeof(EntitySpawnEventPayload));
			registry.Register(typeof(PlayerSpawnResponsePayload));
			registry.Register(typeof(PlayerSpawnRequestPayload));
			registry.Register(typeof(PlayerMoveRequestPayload));
			registry.Register(typeof(EntityPositionUpdateEvent));
			registry.Register(typeof(EntityInteractionRequestPayload));
			registry.Register(typeof(EntityStateChangedEvent));

		}

		protected override ClientPeerSession CreateIncomingPeerSession(INetworkMessageRouterService sender, IConnectionDetails details, INetworkMessageSubscriptionService subService, IDisconnectionServiceHandler disconnectHandler, INetworkMessageRouteBackService routebackService)
		{
			//For now we assume that any connection is valid.
			//In the future we might filter out some IPs but most validation will be post-connection from a signed message.
			ClientPeerSession session = new InstanceClientSession(Logger, sender, details, subService, disconnectHandler, routebackService, instanceSessionRequestMessageHandlerService);

			//Register the OnDisconnect UnityEvent for when the session disconnects
			disconnectHandler.DisconnectionEventHandler += () => OnSessionDisconnected?.Invoke(session);

			//This isn't great; invoking this before returning it to the server base is not great design but it's the only way right now
			OnNewSessionCreated?.Invoke(session);

			return session;
		}
	}
}
