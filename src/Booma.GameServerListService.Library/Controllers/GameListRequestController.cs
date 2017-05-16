using Booma.Payloads.ServerSelection;
using GladNet.ASP.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GladNet.Payload;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Booma.GameServerList.Lib
{
	/// <summary>
	/// Controller that handles GladNet <see cref="GameServerListRequestPayload"/>s.
	/// </summary>
	[PayloadRoute(typeof(GameServerListRequestPayload))]
	public class GameListRequestController : RequestController<GameServerListRequestPayload>
	{
		/// <summary>
		/// Gameserver details repo.
		/// </summary>
		private IGameServerDetailsRepositoryAsync GameserverDetailsRepoService { get; }

		/// <summary>
		/// The logging service.
		/// </summary>
		private ILogger LoggingService { get; }

		public GameListRequestController([NotNull] IGameServerDetailsRepositoryAsync repo, [NotNull] ILogger<GameListRequestController> logger)
		{
			if (repo == null) throw new ArgumentNullException(nameof(repo));
			if (logger == null) throw new ArgumentNullException(nameof(logger));

			GameserverDetailsRepoService = repo;
			LoggingService = logger;
		}

		/// <summary>
		/// Called by GladNet when a <see cref="GameServerListRequestPayload"/> is recieved by the
		/// ASP controller.
		/// </summary>
		/// <param name="payloadInstance"></param>
		/// <returns>A <see cref="GameServerListResponsePayload"/>.</returns>
		
		protected override async Task<PacketPayload> HandlePost([NotNull] GameServerListRequestPayload payloadInstance)
		{
			//Shouldn't ever be null though
			if (payloadInstance == null) throw new ArgumentNullException(nameof(payloadInstance));

			GameServerListResponseCode responseCode = GameServerListResponseCode.Unknown;

			//Force enumerate only once
			GameServerDetailsModel[] details = (await GameserverDetailsRepoService.GetAllPublic()).ToArray();

			if(LoggingService.IsEnabled(LogLevel.Critical))
				LoggingService.LogCritical($"Found: {details.Length}.");

			//Check if we have any servers
			responseCode = !details.Any() ? GameServerListResponseCode.ServiceUnavailable : GameServerListResponseCode.Success;

			SimpleGameServerDetailsModel[] detailsList = details.Select(d => new SimpleGameServerDetailsModel(d.Name, IPAddress.Parse(d.Address), d.ServerPort, d.Region)).ToArray();

			if (LoggingService.IsEnabled(LogLevel.Critical))
				LoggingService.LogCritical($"Found: {detailsList.Length}.");

			//Build a response payload and map the DB model to the wire-type model.
			return new GameServerListResponsePayload(responseCode, detailsList);
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			IEnumerable<GameServerDetailsModel> details = await GameserverDetailsRepoService.GetAllPublic();

			if(details == null)
				throw new InvalidOperationException($"Queried {nameof(IEnumerable<GameServerDetailsModel>)} is null. Failed to load from {nameof(GameserverDetailsRepoService)} repo.");

			return new JsonResult(details);
		}
	}
}
