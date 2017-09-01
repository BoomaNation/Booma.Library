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
			if (authModel == null || !ModelState.IsValid)
				return Json(new JWTModel($"Failed to authenticate.", "Invalid request."));

			if(String.IsNullOrWhiteSpace(authModel.username))
				return Json(new JWTModel($"Failed to authenticate.", "Username must be valid."));

			//We should ask Gaia about this user. First enforce that it's not a number
			int i;
			if (int.TryParse(authModel.username, out i))
				return Json(new JWTModel($"Failed to authenticate.", "Username must be used. Not the user id."));

			try
			{
				bool wasFoundInDataStorage = await GaiaNameRepo.DoesEntryExist(authModel.username);

				int? userId = !wasFoundInDataStorage ? await GetUserIdFrom(authModel.username, QueryClient) 
					: await GetUserIdFrom(authModel.username, GaiaNameRepo);

				if(!userId.HasValue)
					return Json(new JWTModel($"Failed to authenticate.", $"Failed to query for the User: {authModel.username}. This user is unavailable or doesn't exist."));

				//If we don't have the entry
				if(!wasFoundInDataStorage)
					if(!await GaiaNameRepo.InsertEntry(authModel.username, userId.Value))
						throw new InvalidOperationException($"Failed to add name entry UserName: {authModel.username} with Id: {userId.Value}.");

				return Json(new JWTModel(userId.Value.ToString())); //we'll use user id temporarily as the accesstoken
			}
			catch (Exception e)
			{
				//TODO: This could leak sensitive information. Only use this in dev
				return Json(new JWTModel("Failed to query Gaia for user info.", e.Message));
			}
		}

		private async Task<int?> GetUserIdFrom([NotNull] string userName, [NotNull] IGaiaNameRepository dataService)
		{
			if(dataService == null) throw new ArgumentNullException(nameof(dataService));
			if(string.IsNullOrWhiteSpace(userName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(userName));

			return await dataService.GetIdByName(userName);
		}

		private async Task<int?> GetUserIdFrom([NotNull] string userName, [NotNull] IGaiaOnlineQueryClient client)
		{
			if(client == null) throw new ArgumentNullException(nameof(client));
			if(string.IsNullOrWhiteSpace(userName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(userName));

			UserAvatarQueryResponse response = await client.GetAvatarFromUsername(userName);

			if(!response.isRequestSuccessful)
				return null;

			return Int32.Parse(response.UserId);
		}
	}
}
