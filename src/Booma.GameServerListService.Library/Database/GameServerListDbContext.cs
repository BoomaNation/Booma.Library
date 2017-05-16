using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using System.Net;
using Booma.ServerSelection.Common;

namespace Booma.GameServerList.Lib
{
	/// <summary>
	/// Context for the gameserver list.
	/// </summary>
	public sealed class GameServerListDbContext : DbContext
	{
		/// <summary>
		/// The available gameservers.
		/// </summary>
		public DbSet<GameServerDetailsModel> GameServers { get; private set; } //do not remove setter; ASP needs it

		public GameServerListDbContext(DbContextOptions options) 
			: base(options)
		{
			if(GameServers == null)
				throw new InvalidOperationException($"Internally managed {nameof(GameServers)} {nameof(DbSet<GameServerDetailsModel>)} is null. ASP should have initialized it.");

			//Uncomment for test
			if (!GameServers.Any())
			{
				GameServers.Add(new GameServerDetailsModel() { Address = IPAddress.Any.ToString(), Name = "Vega", Region = ServerRegion.NA, ServerPort = 55, Status = ServerStatus.Online | ServerStatus.Public });
				GameServers.Add(new GameServerDetailsModel() { Address = IPAddress.Any.ToString(), Name = "Fodra", Region = ServerRegion.JP, ServerPort = 55, Status = ServerStatus.Online | ServerStatus.Public });
				GameServers.Add(new GameServerDetailsModel() { Address = IPAddress.Any.ToString(), Name = "Ephinea", Region = ServerRegion.EU, ServerPort = 55, Status = ServerStatus.Online | ServerStatus.Public });
				SaveChanges();
			}
		}
	}
}
