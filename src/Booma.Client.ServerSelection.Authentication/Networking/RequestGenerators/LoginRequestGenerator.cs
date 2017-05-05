using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Common;
using GladBehaviour.Common;
using SceneJect.Common;
using System.Security.Cryptography;
using GladLive.Security.Common;
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
	public class LoginRequestGenerator : GladMonoBehaviour, IRequestSender
	{
		//TODO: Put this in a base class.
		[SerializeField]
		private INetPeer messageSender;

		/// <summary>
		/// User provided login details for the attempted login.
		/// </summary>
		[Inject]
		private ILoginDetails loginDetails;

		public void Start()
		{
			if(messageSender == null)
				throw new InvalidOperationException($"Field {nameof(messageSender)} was null.");

			if(loginDetails == null)
				throw new InvalidOperationException($"Field {nameof(loginDetails)} was null.");
		}

		public void SendRequest()
		{
			SendRequestWithResult();
		}

		public SendResult SendRequestWithResult()
		{
			SendResult result = messageSender.TrySendMessage(OperationType.Request, new AuthenticationRequest(loginDetails.LoginString, loginDetails.Password), DeliveryMethod.ReliableOrdered, true);

			//We don't need the login object anymore
			Destroy(loginDetails as UnityEngine.Object);

			return result;
		}
	}
}
