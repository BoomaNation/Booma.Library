using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Booma.GameServerList.Lib
{
	public class GameServerDetailsRepository : IGameServerDetailsRepositoryAsync
	{
		/// <summary>
		/// Database context.
		/// </summary>
		private GameServerListDbContext GameserverListContext { get; }

		/// <summary>
		/// Creates a new gameserver repo.
		/// </summary>
		/// <param name="context">Database context.</param>
		public GameServerDetailsRepository([NotNull] GameServerListDbContext context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			GameserverListContext = context;
		}

		public async Task<IEnumerable<GameServerDetailsModel>> GetAllPublic()
		{
			return await GameserverListContext.GameServers
				.ToAsyncEnumerable()
				.Where(gs => gs.Status.HasFlag(ServerStatus.Public | ServerStatus.Online)) //select online public servers
				.ToList();
		}
	}
}
