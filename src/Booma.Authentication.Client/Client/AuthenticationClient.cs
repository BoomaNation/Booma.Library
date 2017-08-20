using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using HaloLive.Models.Authentication;
using HaloLive.Network;
using SceneJect.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Booma.Client
{
	[Injectee]
	public sealed class AuthenticationClient : MonoBehaviour
	{
		[Serializable]
		public class FailedAuthenticationUnityEvent : UnityEvent<string> { } 

		/// <summary>
		/// The logging service for this component.
		/// </summary>
		[Inject]
		private readonly ILog Logger;

		/// <summary>
		/// The authentication service.
		/// </summary>
		[Inject]
		private readonly IAuthenticationService AuthService;

		/// <summary>
		/// The authentication details provider.
		/// </summary>
		[Inject]
		private readonly IUserAuthenticationDetailsContainer AuthDetailsContainer;

		/// <summary>
		/// Event to be invoked when authentication is successful.
		/// </summary>
		[SerializeField]
		[Tooltip("Event to be invoked when authentication is successful.", order = 1)]
		private UnityEvent OnSuccessfulAuthentication;

		/// <summary>
		/// Event to be invoked when the authentication has failed.
		/// </summary>
		[SerializeField]
		[Tooltip("Event to be invoked when the authentication has failed.", order = 2)]
		private FailedAuthenticationUnityEvent OnFailedAuthentication;

		void Start()
		{
			if (Logger == null) throw new ArgumentNullException(nameof(Logger));
			if (AuthService == null) throw new ArgumentNullException(nameof(AuthService));
			if (AuthDetailsContainer == null) throw new ArgumentNullException(nameof(AuthDetailsContainer));
		}

		public void Authenticate()
		{
			//Just try to authentication and continue with calling the events of success or failure.
			AuthService.TryAuthenticate(new AuthenticationRequestModel(AuthDetailsContainer.UserName, AuthDetailsContainer.Password))
				.UnityAsyncContinueWith(this, LogAndDipatchAuthResult);
		}

		private Task<string> ReadAccessToken(JWTModel model)
		{
			return null;
		}

		private string TestString(JWTModel model)
		{
			return null;
		}

		private void LogAndDipatchAuthResult(JWTModel result)
		{
			if (result == null)
				throw new InvalidOperationException($"Encountered invalid {nameof(JWTModel)} after authentication.");

			if (result.isTokenValid)
			{
				if (Logger.IsDebugEnabled)
					Logger.Debug($"Recieved a valid JWT access token.");

				OnSuccessfulAuthentication?.Invoke();
			}
			else
			{
				if (Logger.IsDebugEnabled)
					Logger.Debug($"Failed to authenticate. {result.Error}: {result.ErrorDescription}.");

				OnFailedAuthentication?.Invoke($"Failed to authenticate. {result.Error}: {result.ErrorDescription}.");
			}
		}
	}
}
