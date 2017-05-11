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
	/// <summary>
	/// A network peer component that is used to connect to the authentication service.
	/// </summary>
	[Injectee]
	public class AuthenticationWebClient : BoomaNetworkWebPeer<AuthenticationWebClient>
	{
		/// <summary>
		/// Unity3D dispatchable event that is called when connection is established.
		/// </summary>
		[SerializeField]
		private UnityEvent OnConnected;

		public override void Start()
		{
			base.Start();

			MiddlewareRegistry.RegisterAuthenticationMiddleware(this.serializer, this.deserializer, new JWTTokenServiceManager());

			//Web peers connect right away
			OnConnected?.Invoke();
		}

		/// <inheritdoc />
		protected override void RegisterPayloads(ISerializerRegistry registeryService)
		{
			registeryService.RegisterAuthenticationPayloads();
			registeryService.Register(typeof(NetworkMessage));
			registeryService.Register(typeof(RequestMessage));
			registeryService.Register(typeof(ResponseMessage));
			registeryService.RegisterServerSelectionPayloads();
		}
	}
}
