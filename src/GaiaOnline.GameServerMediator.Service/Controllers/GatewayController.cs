using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaloLive.Models.NameResolution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GaiaOnline
{
	[Route("api/[controller]")]
	public sealed class GatewayController : Controller
	{
		//they send an OAuth token, or they should, but right now we actually don't.
		//So we pretend we do and just send information in the token
		//TODO: When we switch to OAuth enable
		//[Authorize]
		[HttpPost("enter")]
		public async Task<JsonResult> TryEnterGameServer([FromServices] IGameSessionRepository gameSessionRepository)
		{
			if(!ModelState.IsValid)
				return new JsonResult(new ServerEntryResponse(ServerEntryResponseCode.FailedConnectionActivelyRefused));

			//TODO: Once this is OAuth2 we can't just parse the token like this
			int? potentialId = TryGetUserId(Request);

			if(!potentialId.HasValue)
				return new JsonResult(new ServerEntryResponse(ServerEntryResponseCode.FailedConnectionActivelyRefused));

			int userId = potentialId.Value;
	
			bool hasSessionAlready = await gameSessionRepository.HasSession(userId);

			if(hasSessionAlready)
				return new JsonResult(new ServerEntryResponse(ServerEntryResponseCode.FailedAlreadyLoggedIn));

			//This could result in a data race so we need to check the result after we create. Don't assume this won't fail or be exploited
			SessionCreationResult result = await gameSessionRepository.TryCreateSession(userId, Request.HttpContext.Connection.RemoteIpAddress.ToString());

			if(!result.isSessionCreated)
				return new JsonResult(new ServerEntryResponse(ServerEntryResponseCode.GeneralServerError));

			//TODO: Handle gameserver/zoneserver redirection based on session information.
			return new JsonResult(new ServerEntryResponse(result.SessionGuid, new ResolvedEndpoint("127.0.0.1", 8051))); //TODO: Obviously we want to look in the DB and provide a real token.
		}

		public int? TryGetUserId(HttpRequest request)
		{
			//TODO: When we enable actual OAuth we won't need to manually read this
			if(Request.Headers.ContainsKey("Authorization"))
				return null;

			string token = Request.Headers["Authorization"].FirstOrDefault();

			if(String.IsNullOrWhiteSpace(token))
				return null;

			int id;
			int.TryParse(token, out id);

			return id;
		}
	}
}
