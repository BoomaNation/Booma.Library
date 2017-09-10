using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaloLive.Hosting;
using HaloLive.Models.NameResolution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booma
{
	[Route("api/[controller]")]
	public sealed class GatewayController : AuthorizationReadyController
	{
		//they send an OAuth token, or they should, but right now we actually don't.
		//So we pretend we do and just send information in the token
		/// <inheritdoc />
		public GatewayController([FromServices] IClaimsPrincipalReader haloLiveUserManager) 
			: base(haloLiveUserManager)
		{

		}

		/// <summary>
		/// Action that users call to try to enter into the gameserver. They may call this after selecting something from the gameserver list.
		/// It will attempt to find where they are in the world and create a session on that zone/sub server. This session creation is what grants them
		/// access to the subserver/zone. Otherwise they will be rejected or cannot enter since they have no where they can be connected to.
		/// </summary>
		/// <param name="gameSessionRepository">The game session repository service.</param>
		/// <returns></returns>
		[Authorize]
		[HttpPost("enter")]
		public async Task<JsonResult> TryEnterGameServer([FromServices] IGameSessionRepository gameSessionRepository)
		{
			if(!ModelState.IsValid)
				return Json(new ServerEntryResponse(ServerEntryResponseCode.FailedConnectionActivelyRefused));

			int userId = int.Parse(HaloLiveUserManager.GetUserId(User)); //TODO: Should we try parse? Or change the signature for this?
	
			//If there is a session and we're already logged in reject the requesting user
			//It is possible for there to be an existing session yet not logged in
			//that would mean an existing session was unclaimined and hasn't been cleaned up yet or someone is connecting in the middle of the session transfer
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

		//TODO: We should authorize instance servers trying to claim sessions
		[HttpPost("validate")]
		public async Task<JsonResult> TryClaimSession([FromBody] ServerSessionClaimRequest sessionInquiryRequest, [FromServices] IGameSessionRepository gameSessionRepository)
		{
			//TODO: The session could be removed? We may do that when they log out. Or if they transfer.
			if(!await gameSessionRepository.HasSession(sessionInquiryRequest.SessionGuid))
				return Json(new ServerSessionClaimResponse(ServerSessionClaimResponseCode.FailedNoSessionRegistered));

			//Could be a race condition here in the future when the session logic is fully implemented
			//We need to verify for the requesting gameserver that the IP matches
			string ip = (await gameSessionRepository.GetSessionByGuid(sessionInquiryRequest.SessionGuid)).SessionIp;

			//This could happen if a malicious user was trying to claim random sessions.
			//This doesn't exactly prevent someone from stealing known sessions on the same network though.
			if(ip != sessionInquiryRequest.IpAddress)
				return Json(new ServerSessionClaimResponse(ServerSessionClaimResponseCode.FailedSessionIsForDifferentIp));
			
			//At this point we need to try to claim the session
			if(await gameSessionRepository.TryClaimSession(sessionInquiryRequest.SessionGuid))
				//TODO: There is a lot more stuff we NEED to do in the future. We need to validate that this is the server the session was created on, that they aren't logged in and etc.
				return Json(new ServerSessionClaimResponse(ServerSessionClaimResponseCode.Success));

			//TODO: We should add more information and logging
			return Json(new ServerSessionClaimResponse(ServerSessionClaimResponseCode.FailedGeneralServerError));
		}
	}
}
