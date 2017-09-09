using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Booma
{
	/// <summary>
	/// Name query/entry database context.
	/// </summary>
	public sealed class GaiaNameQueryDatabaseContext : DbContext
	{
		/// <summary>
		/// Set of entries in the database.
		/// </summary>
		public DbSet<GaiaNameEntryModel> NameEntries { get; set; }

		public GaiaNameQueryDatabaseContext(DbContextOptions<GaiaNameQueryDatabaseContext> options) 
			: base(options)
		{

		}

		public GaiaNameQueryDatabaseContext()
		{

		}

#if DEBUG || DEBUGBUILD
		/// <inheritdoc />
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySql("Server=localhost;Database=GaiaOnlineMMO;Uid=root;Pwd=test;");
		}
#endif
	}
}
