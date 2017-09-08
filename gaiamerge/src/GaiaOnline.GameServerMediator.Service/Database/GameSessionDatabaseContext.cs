using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace GaiaOnline
{
	public sealed class GameSessionDatabaseContext : DbContext
	{
		/// <summary>
		/// The object to interface with the game session database table.
		/// </summary>
		public DbSet<GameSessionModel> GameSessions { get; set; }

		public GameSessionDatabaseContext(DbContextOptions<GameSessionDatabaseContext> options) 
			: base(options)
		{

		}

		public GameSessionDatabaseContext()
		{

		}
	}
}
