using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaloLive.Models.Authentication;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using TypeSafe.Http.Net;

namespace GaiaOnline
{
	//We are mocking HaloLive's IAuthenticationService.
	//We're pretending to be an OAuth service. We can't authenticate because we aren't Gaia
	//But we can block people from connecting who try to be a fake avatar.
	[Route("api/auth")]
	public class AuthenticationController : Controller
	{
		//We need to do this because we can't easily map url encoded body to JSON object
		//Normally this is handled by OpenIddict/OAuth library but we're mocking that tat the moment.
		public sealed class AuthRequestUrlEncodedMock
		{
			public string username { get; set; }

			public string password { get; set; }

			public string grant_type { get; set; }
		}

		/// <summary>
		/// Gaia query client.
		/// </summary>
		private IGaiaOnlineQueryClient QueryClient { get; }

		public IGaiaNameRepository GaiaNameRepo { get; }

		public AuthenticationController([FromServices] IGaiaOnlineQueryClient queryClient, [FromServices] IGaiaNameRepository gaiaNameRepo)
		{
			if (queryClient == null) throw new ArgumentNullException(nameof(queryClient));
			if (gaiaNameRepo == null) throw new ArgumentNullException(nameof(gaiaNameRepo));

			QueryClient = queryClient;
			GaiaNameRepo = gaiaNameRepo;
		}

		[HttpPost]
		public async Task<JsonResult> Authenticate(AuthRequestUrlEncodedMock authModel) //don't
		{
			if (authModel == null || !ModelState.IsValid) throw new ArgumentException(nameof(authModel));

			//We should ask Gaia about this user. First enforce that it's not a number
			int i;
			if (int.TryParse(authModel.username, out i))
				return null; //TODO: Send back error if they try to log in with userid

			try
			{
				UserAvatarQueryResponse response = await QueryClient.GetAvatarFromUsername(authModel.username);

				if(!response.isRequestSuccessful)
					return new JsonResult(new JWTModel($"Failed error code: {response.ResponseStatusCode}", $"Failed to query for the User: {authModel.username}. This user is unavailable or doesn't exist."));

				int userId = int.Parse(response.UserId);

				//If we don't have the entry
				if (!await GaiaNameRepo.DoesEntryExist(userId))
					if(!await GaiaNameRepo.InsertEntry(authModel.username, userId))
						throw new InvalidOperationException($"Failed to add name entry UserName: {authModel.username} with Id: {response.UserId}.");

				return new JsonResult(new JWTModel(response.UserId)); //we'll use user id temporarily as the accesstoken
			}
			catch (Exception e)
			{
				//TODO: This could leak sensitive information. Only use this in dev
				return new JsonResult(new JWTModel("Failed to query Gaia for user info.", e.Message));
			}
		}
	}
}
