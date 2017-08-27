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
		public async Task<JsonResult> TryEnterGameServer()
		{
			if(!ModelState.IsValid)
				return new JsonResult(new ServerEntryResponse(ServerEntryResponseCode.FailedConnectionActivelyRefused));

			//TODO: When we enable actual OAuth we won't need to manually read this
			if(Request.Headers.ContainsKey("Authorization"))
				return new JsonResult(new ServerEntryResponse(ServerEntryResponseCode.FailedConnectionActivelyRefused));

			string token = Request.Headers["Authorization"].FirstOrDefault();

			if(token == null)
				return new JsonResult(new ServerEntryResponse(ServerEntryResponseCode.FailedConnectionActivelyRefused));

			//TODO: In a real server we should check the database for if they are logged in
			//TODO: If they are we should reject. Otherwise we need to find out where they are in the world
			//TODO: Because if there are multiple zone servers, or instance servers, we need to create a session on for them on those
			//TODO: And redirect them to the proper zone
			return new JsonResult(new ServerEntryResponse(Guid.NewGuid(), new ResolvedEndpoint("127.0.0.1", 8051))); //TODO: Obviously we want to look in the DB and provide a real token.
		}
	}
}
