using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace GaiaOnline
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

		public GaiaNameQueryDatabaseContext(DbContextOptions options) 
			: base(options)
		{

		}
	}
}
