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

		//TODO: Validate services and auth details
		public void AuthenticateUser()
		{
			AuthService.TryAuthenticate(new AuthenticationRequestModel(AuthDetails.UserName, AuthDetails.Password))
				.UnityAsyncContinueWith(this, OnAuthResponseRecieved);
		}

		private void OnAuthResponseRecieved(JWTModel result)
		{
			if(result == null) throw new ArgumentNullException(nameof(result));

			if(!result.isTokenValid)
				throw new NotImplementedException("TODO: Handle failed authentication.");

			//the result should be valid and we should store the result in the player prefs
			PlayerPrefs.SetString(PlayerPreferences.UserToken.ToString(), result.AccessToken);
		}
	}
}
