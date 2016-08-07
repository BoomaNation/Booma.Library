using GladLive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Common;
using UnityEngine;
using Booma.Client.Network.Common;
using SceneJect.Common;
using GladLive.Common.Payloads;
using UnityEngine.Events;
using GladNet.Message;

namespace Booma.Client.ServerSelection.Authentication
{
	/// <summary>
	/// Response handler that handless <see cref="LoginResponse"/> payloads.
	/// </summary>
	public class LoginResponseHandler : ResponsePayloadHandlerComponent<LoginNetworkClient, LoginResponse>
	{
		/// <summary>
		/// Serializable hack for getting UnityEvents in the Unity inspector
		/// </summary>
		[Serializable]
		public class Event : UnityEvent<LoginResponseCode> { }

		/// <summary>
		/// Invoked on a failed <see cref="LoginResponse"/> handled.
		/// </summary>
		[SerializeField]
		private Event OnFailure;

		/// <summary>
		/// Invoked on a sucessful <see cref="LoginResponse"/> handled.
		/// </summary>
		[SerializeField]
		private Event OnSuccess;

		protected override void OnIncomingHandlableMessage(IResponseMessage message, LoginResponse payload, IMessageParameters parameters, LoginNetworkClient peer)
		{
			//Log out the response first
			Logger.Debug($"Recieved response code: {payload.Code}");


			switch (payload.Code)
			{
				case LoginResponseCode.Success:
					if (OnSuccess != null)
						OnSuccess.Invoke(payload.Code);
					break;
				case LoginResponseCode.Banned:
				case LoginResponseCode.Locked:
				case LoginResponseCode.Failed:
				case LoginResponseCode.AuthServerUnavailable:
					if (OnFailure != null)
						OnFailure.Invoke(payload.Code);
					break;
				default:
					throw new InvalidOperationException($"Recieved invalid or unknown response code {payload.Code}.");
			}
		}
	}
}
