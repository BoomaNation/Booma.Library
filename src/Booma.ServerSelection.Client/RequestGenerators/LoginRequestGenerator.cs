using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Common;
using GladBehaviour.Common;
using SceneJect.Common;
using System.Security.Cryptography;
using Booma.Unity.Network;
using GladNet.Engine.Common;
using UnityEngine;
using GladNet.Payload.Authentication;

namespace Booma.Client.ServerSelection.Authentication
{
	/// <summary>
	/// Component that can implements <see cref="IRequestSender"/> which can be used
	/// to send requests to a <see cref="IClientPeerNetworkMessageSender"/>.
	/// </summary>
	[Injectee]
	public class LoginRequestGenerator : RequestGenerator
	{
		/// <summary>
		/// User provided login details for the attempted login.
		/// </summary>
		[Inject]
		private readonly ILoginDetails loginDetails;

		/// <inheritdoc />
		protected override void OnStart()
		{
			if (loginDetails == null)
				throw new InvalidOperationException($"Field {nameof(loginDetails)} was null.");
		}

		public override void SendRequest()
		{
			SendRequestWithResult();
		}

		public override SendResult SendRequestWithResult()
		{
			SendResult result = NetworkPeer.TrySendMessage(OperationType.Request, new AuthenticationRequest(loginDetails.LoginString, loginDetails.Password), DeliveryMethod.ReliableOrdered, true);

			//We don't need the login object anymore
			Destroy(loginDetails as UnityEngine.Object);

			return result;
		}
	}
}
