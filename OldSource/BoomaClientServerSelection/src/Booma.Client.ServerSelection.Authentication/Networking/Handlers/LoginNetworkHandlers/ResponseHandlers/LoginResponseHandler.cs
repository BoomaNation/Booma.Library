using GladLive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladNet.Common;
using UnityEngine;
using Booma.Client.Network.Common;
using Common.Logging;
using SceneJect.Common;
using UnityEngine.Events;
using GladNet.Message;
using GladNet.Payload.Authentication;

namespace Booma.Client.ServerSelection.Authentication
{
	/// <summary>
	/// Response handler that handless <see cref="LoginResponse"/> payloads.
	/// </summary>
	[Injectee]
	public class LoginResponseHandler : ResponsePayloadHandlerComponent<AuthenticationWebClient, AuthenticationResponse>
	{
		/// <summary>
		/// Serializable hack for getting UnityEvents in the Unity inspector
		/// </summary>
		[Serializable]
		public class ErrorEvent : UnityEvent<ResponseErrorCode, string> { }

		/// <summary>
		/// Invoked on a failed <see cref="LoginResponse"/> handled.
		/// </summary>
		[SerializeField]
		private ErrorEvent OnFailure;

		/// <summary>
		/// Invoked on a sucessful <see cref="LoginResponse"/> handled.
		/// </summary>
		[SerializeField]
		private UnityEvent OnSuccess;

		[Inject]
		private ILog Logger;

		protected override void OnIncomingHandlableMessage(IResponseMessage message, AuthenticationResponse payload, IMessageParameters parameters, AuthenticationWebClient peer)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Auth result: {payload.AuthenticationSuccessful}");

			if (payload.AuthenticationSuccessful)
				OnSuccess?.Invoke();
			else
				OnFailure?.Invoke(payload.OptionalError ?? ResponseErrorCode.Error, payload.OptionalErrorMessage);
		}
	}
}
