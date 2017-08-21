using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GaiaOnline
{
	public class DatabaseGameServerListRepository : IGameServerListRepository
	{
		private GameServerListDbContext Context { get; }

		public DatabaseGameServerListRepository([FromServices] GameServerListDbContext context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			Context = context;
		}

		/// <inheritdoc />
		public async Task<IEnumerable<GameServerListEntryModel>> RetrieveAll()
		{
			return await Context.Endpoints.ToListAsync();
		}
	}
}
