using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace Booma
{
	public sealed class DbContextBasedGaiaNameRepository : IGaiaNameRepository
	{
		private GaiaNameQueryDatabaseContext DatabaseContext { get; }

		public ILogger<DbContextBasedGaiaNameRepository> Logger { get; }

		public DbContextBasedGaiaNameRepository(GaiaNameQueryDatabaseContext databaseContext, ILogger<DbContextBasedGaiaNameRepository> logger)
		{
			if (databaseContext == null) throw new ArgumentNullException(nameof(databaseContext));
			if (logger == null) throw new ArgumentNullException(nameof(logger));

			DatabaseContext = databaseContext;
			Logger = logger;
		}

		/// <inheritdoc />
		public async Task<string> GetNameById(int userId)
		{
			//Use AsNoTracking so that we always get live up-to-date results
			return (await DatabaseContext.NameEntries.FirstAsync(n => n.UserId == userId)).AvatarUsername;
		}

		/// <inheritdoc />
		public async Task<int> GetIdByName(string avatarName)
		{
			return (await DatabaseContext.NameEntries.FirstAsync(n => n.AvatarUsername == avatarName)).UserId;
		}

		/// <inheritdoc />
		public async Task<bool> DoesEntryExist(int userId)
		{
			//Use AsNoTracking so that we always get live up-to-date results
			return await DatabaseContext.NameEntries.AnyAsync(n => n.UserId == userId);
		}

		/// <inheritdoc />
		public async Task<bool> DoesEntryExist(string avatarName)
		{
			return await DatabaseContext.NameEntries.AnyAsync(n => n.AvatarUsername == avatarName);
		}

		/// <inheritdoc />
		public async Task<bool> InsertEntry(string avatarUsername, int userId)
		{
			//If I understand correctly there could be a race condition here.
			//But instead of table locking and reducing throughput we can gracefully catch and supress the exception
			try
			{
				await DatabaseContext.NameEntries.AddAsync(new GaiaNameEntryModel(userId, avatarUsername));

				DatabaseContext.SaveChanges();
			}
			catch (DbUpdateException e) //TODO: don't think we need to handle DbUpdateConcurrencyException but we may
			{
				//TODO: Handle other providers
				if (e.InnerException is MySqlException mysqlEx)
				{
					if (mysqlEx.Number == 1062) //1062 is duplicate error code
					{
						if (Logger.IsEnabled(LogLevel.Information))
							Logger.LogInformation($"Insert for NameQuery table failed. May be race condition. ErrorCode: {mysqlEx.Number} Error: {mysqlEx.Message}");

						//Just assume the database has it.
						return true;
					}
				}

				throw; //If it's not in the database some other error happened and we need to rethrow
			}

			//pretend it succeeded even if it fails
			return true;
		}
	}
}
