using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaloLive.Models.NameResolution;
using Microsoft.AspNetCore.Mvc;

namespace GaiaOnline
{
	[Route("api/gameserver")]
	public class GameServerListController : Controller
	{
		private IGameServerListRepository GameServerRepository { get; }

		public GameServerListController([FromServices] IGameServerListRepository gameServerRepository)
		{
			if (gameServerRepository == null) throw new ArgumentNullException(nameof(gameServerRepository));

			GameServerRepository = gameServerRepository;
		}

		/// <summary>
		/// Based on the client interface IGameServiceListQueryClient request method.
		/// </summary>
		/// <returns>The list of gameservers.</returns>
		[HttpGet("list")]
		public async Task<JsonResult> GetGameServerList() //no args are needed.
		{
			IEnumerable<GameServerListEntryModel> entryModels = await GameServerRepository.RetrieveAll();

			if (!entryModels.Any())
				return Json(new GameServerListResponse(GameServerListResponseCode.NoGameServersAvailable));

			return Json(new GameServerListResponse(entryModels.Select(gs => new GameServerInfo(gs.ServerName, gs.Region, new ResolvedEndpoint(gs.EndpointAddress, gs.EndpointPort))).ToList()));
		}
	}
}
