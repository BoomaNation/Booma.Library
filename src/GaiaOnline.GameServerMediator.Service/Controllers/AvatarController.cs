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
	public sealed class AvatarController : Controller
	{
		/// <summary>
		/// Services name query requests where the provided id in the route represents the <see cref="entityId"/>.
		/// This entity id should match the user id from Gaia. Therefore the user can be looked up from it.
		/// </summary>
		/// <param name="gaiaNameRepository">The gaia name data service.</param>
		/// <param name="entityId">The id.</param>
		/// <returns>A JSON serialized <see cref="NameQueryResponse"/> result.</returns>
		[HttpGet("namequery/{id}")]
		public async Task<JsonResult> GetAvatarNameById([FromServices] IReadonlyGaiaNameRepository gaiaNameRepository, [FromRoute(Name = "id")] int entityId)
		{
			if(gaiaNameRepository == null) throw new ArgumentNullException(nameof(gaiaNameRepository));
			if(entityId <= 0) throw new ArgumentOutOfRangeException(nameof(entityId));
			if(!ModelState.IsValid) throw new InvalidOperationException($"The model state is invalid for {nameof(GetAvatarNameById)}.");

			//Users may send namequeries that are invalid, just like WoW, so ignore those.
			if(!await gaiaNameRepository.DoesEntryExist(entityId))
				return Json(new NameQueryResponse(NameQueryResponseCode.UnknownUserId));

			string avatarName = await gaiaNameRepository.GetNameById(entityId);

			return Json(new NameQueryResponse(avatarName));
		}
	}
}
