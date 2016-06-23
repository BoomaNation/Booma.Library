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
using GladLive.Security.Common;

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
		private IClientPeerNetworkMessageSender messageSender;

		/// <summary>
		/// RSA Cryptoservice for encrypting passwords before sending them.
		/// </summary>
		[Inject]
		private ICryptoService cryptoService;

		/// <summary>
		/// User provided login details for the attempted login.
		/// </summary>
		[Inject]
		private ILoginDetails loginDetails;

		public void SendRequest()
		{
			SendRequestWithResult();
		}

		public SendResult SendRequestWithResult()
		{
			SendResult result = messageSender.SendRequest(new LoginRequest(loginDetails.LoginString, cryptoService.Encrypt(loginDetails.Password)), DeliveryMethod.ReliableUnordered, true);

			//We don't need the login object anymore
			Destroy(loginDetails as UnityEngine.Object);

			return result;
		}
	}
}
