using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GaiaOnline
{
	public sealed class DbContextBasedGaiaNameRepository : IGaiaNameRepository
	{
		private GaiaNameQueryDatabaseContext DatabaseContext { get; }

		public DbContextBasedGaiaNameRepository(GaiaNameQueryDatabaseContext databaseContext)
		{
			if (databaseContext == null) throw new ArgumentNullException(nameof(databaseContext));

			DatabaseContext = databaseContext;
		}

		/// <inheritdoc />
		public async Task<string> GetNameById(int userId)
		{
			return (await DatabaseContext.NameEntries.FirstAsync(n => n.UserId == userId))?.AvatarUsername;
		}

		/// <inheritdoc />
		public async Task<bool> DoesEntryExist(int userId)
		{
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
			await DatabaseContext.NameEntries.AddAsync(new GaiaNameEntryModel(userId, avatarUsername));

			//If I understand correctly there could be a race condition here.
			//But instead of table locking and reducing throughput we can gracefully catch and supress the exception
			try
			{
				DatabaseContext.SaveChanges();
			}
			catch (DbUpdateException e) //TODO: don't think we need to handle DbUpdateConcurrencyException but we may
			{
				//If we get an exception it is hopefully from the very race chance that the table already has it due to race condition
				if (!await DoesEntryExist(userId))
					throw; //If it's not in the database some other error happened and we need to rethrow
			}

			//pretend it succeeded even if it fails
			return true;
		}
	}
}
