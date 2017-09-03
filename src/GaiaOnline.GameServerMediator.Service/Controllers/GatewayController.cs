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
		/// <summary>
		/// Action that users call to try to enter into the gameserver. They may call this after selecting something from the gameserver list.
		/// It will attempt to find where they are in the world and create a session on that zone/sub server. This session creation is what grants them
		/// access to the subserver/zone. Otherwise they will be rejected or cannot enter since they have no where they can be connected to.
		/// </summary>
		/// <param name="gameSessionRepository">The game session repository service.</param>
		/// <returns></returns>
		[HttpPost("enter")]
		public async Task<JsonResult> TryEnterGameServer([FromServices] IGameSessionRepository gameSessionRepository)
		{
			if(!ModelState.IsValid)
				return Json(new ServerEntryResponse(ServerEntryResponseCode.FailedConnectionActivelyRefused));

			//TODO: Once this is OAuth2 we can't just parse the token like this
			int? potentialId = TryGetUserId(Request);

			if(!potentialId.HasValue)
				return Json(new ServerEntryResponse(ServerEntryResponseCode.FailedConnectionActivelyRefused));

			int userId = potentialId.Value;
	
			bool hasSessionAlready = await gameSessionRepository.HasSession(userId);

			if(hasSessionAlready)
				return Json(new ServerEntryResponse(ServerEntryResponseCode.FailedAlreadyLoggedIn));

			//This could result in a data race so we need to check the result after we create. Don't assume this won't fail or be exploited
			SessionCreationResult result = await gameSessionRepository.TryCreateSession(userId, Request.HttpContext.Connection.RemoteIpAddress.ToString());

			if(!result.isSessionCreated)
				return Json(new ServerEntryResponse(ServerEntryResponseCode.GeneralServerError));

			//TODO: Handle gameserver/zoneserver redirection based on session information.
			return Json(new ServerEntryResponse(result.SessionGuid, new ResolvedEndpoint("127.0.0.1", 8051))); //TODO: Obviously we want to look in the DB and provide a real token.
		}

		//TODO: We should use OAuth to authenticate requests. We MUST make sure they are from actual servers.
		[HttpPost("validate")]
		public async Task<JsonResult> InquireOnSessionDetails([FromBody] SessionClaimInquiryRequest sessionInquiryRequest, [FromServices] IReadOnlyGameSessionRepository gameSessionRepository)
		{
			//TODO: The session could be removed? We may do that when they log out. Or if they transfer.
			if(!await gameSessionRepository.HasSession(sessionInquiryRequest.SessionGuid))
				return Json(new SessionClaimInquiryResponse(SessionClaimInquiryResponseCode.FailedNoSessionRegistered));

			//Could be a race condition here in the future when the session logic is fully implemented
			//We need to verify for the requesting gameserver that the IP matches
			string ip = (await gameSessionRepository.GetSessionByGuid(sessionInquiryRequest.SessionGuid)).SessionIp;

			//This could happen if a malicious user was trying to claim random sessions.
			//This doesn't exactly prevent someone from stealing known sessions on the same network though.
			if(ip != sessionInquiryRequest.IpAddress)
				return Json(new SessionClaimInquiryResponse(SessionClaimInquiryResponseCode.FailedSessionIsForDifferentIp));

			//TODO: There is a lot more stuff we NEED to do in the future. We need to validate that this is the server the session was created on, that they aren't logged in and etc.
			return Json(new SessionClaimInquiryResponse(SessionClaimInquiryResponseCode.Success));
		}

		//TODO: Once we use OAuth we won't need this
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
