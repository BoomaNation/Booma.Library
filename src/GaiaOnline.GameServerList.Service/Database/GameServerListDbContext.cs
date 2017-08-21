using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaloLive.Network.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GaiaOnline
{
	/// <summary>
	/// The <see cref="DbContext"/> that contains the <see cref="GameServerListEntryModel"/> table.
	/// </summary>
	public sealed class GameServerListDbContext : DbContext
	{
		public DbSet<GameServerListEntryModel> Endpoints { get; set; }

		public GameServerListDbContext(DbContextOptions options)
			: base(options)
		{

		}
	}
}
