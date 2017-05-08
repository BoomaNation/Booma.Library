using Booma.Client.Network.Common;
using GladNet.ASP.Client.RestSharp.Middleware.Authentication;
using GladNet.Message;
using GladNet.Serializer;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.ASP.Client;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Client.ServerSelection
{
	public class AuthenticationWebClient : BoomaNetworkWebPeer<AuthenticationWebClient>
	{
		//TODO: Token handling
		[SerializeField]
		private UnityEvent OnConnected;

		[Inject]
		private readonly ISerializerRegistry registry;

		public override void Start()
		{
			base.Start();

			//TODO: Fix fault so we can register middlewares
			MiddlewareRegistry.RegisterAuthenticationMiddleware(this.serializer, this.deserializer, new JWTTokenServiceManager());

			RegisterMessages();
			OnConnected?.Invoke();
		}
		
		private void RegisterMessages()
		{
			//TODO: Extension for message registeration
			registry.RegisterAuthenticationPayloads();
			registry.Register(typeof(NetworkMessage));
			registry.Register(typeof(RequestMessage));
			registry.Register(typeof(ResponseMessage));
			registry.RegisterServerSelectionPayloads();

			//TODO: Add async handling so that we don't need to register here
		}
	}
}
