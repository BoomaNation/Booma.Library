using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using HaloLive.Models.Authentication;
using HaloLive.Network;
using SceneJect.Common;
using UnityEngine;
using UnityEngine.Events;
using Unitysync.Async;

namespace GaiaOnline
{
	[Injectee]
	public sealed class AuthenticationClient : MonoBehaviour
	{
		/// <summary>
		/// Authentication service.
		/// </summary>
		[Inject]
		private IAuthenticationService AuthService { get; }

		[Inject]
		private IUserAuthenticationDetailsContainer AuthDetails { get; }

		[SerializeField]
		private UnityEvent OnRecievedSuccessfulAuthResponse;

		//TODO: Validate services and auth details
		public void AuthenticateUser()
		{
			//TODO: Add the ILogger service
			Debug.Log($"Connecting with User: {AuthDetails.UserName}");

			//Async auth request that dispatches to OnAuthResponseRecieved
			AuthService.TryAuthenticate(new AuthenticationRequestModel(AuthDetails.UserName, AuthDetails.Password))
				.UnityAsyncContinueWith(this, OnAuthResponseRecieved);
		}

		private void OnAuthResponseRecieved(JWTModel result)
		{
			Debug.Log($"Recieved Auth Response Result: {result.isTokenValid}");

			if(result == null) throw new ArgumentNullException(nameof(result));

			if(!result.isTokenValid)
				throw new NotImplementedException("TODO: Handle failed authentication.");

			Debug.Log($"Setting player prefs access token");

			//TODO: maybe extract this into seperate interface
			//the result should be valid and we should store the result in the player prefs
			PlayerPrefs.SetString(PlayerPreferences.UserToken.ToString(), result.AccessToken);

			OnRecievedSuccessfulAuthResponse?.Invoke();
		}
	}
}
