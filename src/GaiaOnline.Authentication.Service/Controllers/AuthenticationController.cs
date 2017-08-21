using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaloLive.Models.Authentication;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace GaiaOnline
{
	//We are mocking HaloLive's IAuthenticationService.
	//We're pretending to be an OAuth service. We can't authenticate because we aren't Gaia
	//But we can block people from connecting who try to be a fake avatar.
	[Route("api/auth")]
	public class AuthenticationController : Controller
	{
		/// <summary>
		/// Gaia query client.
		/// </summary>
		private IGaiaOnlineQueryClient QueryClient { get; }

		public AuthenticationController([FromServices] IGaiaOnlineQueryClient queryClient)
		{
			if (queryClient == null) throw new ArgumentNullException(nameof(queryClient));

			QueryClient = queryClient;
		}

		[HttpPost]
		public async Task<JsonResult> Authenticate([FromBody] AuthenticationRequestModel authModel)
		{
			if (authModel == null || !ModelState.IsValid) throw new ArgumentException(nameof(authModel));

			//We should ask Gaia about this user. First enforce that it's not a number
			int i;
			if (int.TryParse(authModel.UserName, out i))
				return null; //TODO: Send back error if they try to log in with userid

			try
			{
				UserAvatarQueryResponse response = await QueryClient.GetAvatarFromUsername(authModel.UserName);

				if(response.isRequestSuccessful)
					return new JsonResult(new JWTModel(response.UserId)); //we'll use user id temporarily as the accesstoken
				
				return new JsonResult(new JWTModel($"Failed error code: {response.ResponseStatusCode}", $"Failed to query for the User: {authModel.UserName}. This user is unavailable or doesn't exist."));
			}
			catch (Exception e)
			{
				//TODO: This could leak sensitive information. Only use this in dev
				return new JsonResult(new JWTModel("Failed to query Gaia for user info.", e.Message));
			}
		}
	}
}
