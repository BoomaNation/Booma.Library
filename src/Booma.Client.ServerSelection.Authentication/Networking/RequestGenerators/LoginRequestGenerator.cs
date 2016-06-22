using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Common;
using UnityEngine;
using GladBehaviour.Common;
using GladLive.Common.Payloads;
using SceneJect.Common;
using System.Security.Cryptography;

namespace Booma.Client.ServerSelection.Authentication
{
	/// <summary>
	/// Component that can implements <see cref="IRequestSender"/> which can be used
	/// to send requests to a <see cref="IClientPeerNetworkMessageSender"/>.
	/// </summary>
	public class LoginRequestGenerator : GladMonoBehaviour, IRequestSender
	{
		//TODO: Put this in a base class.
		[SerializeField]
		private IClientPeerNetworkMessageSender messageSender;

		/// <summary>
		/// RSA Cryptoservice for encrypting passwords before sending them.
		/// </summary>
		[Inject]
		private RSACryptoServiceProvider cryptoService;

		[Inject]

		public void SendRequest()
		{
			SendRequestWithResult();
		}

		public SendResult SendRequestWithResult()
		{
			return messageSender.SendRequest(new LoginRequest("Test", new byte[0]), DeliveryMethod.ReliableUnordered, true);
		}
	}
}
