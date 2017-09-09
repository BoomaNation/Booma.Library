using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Booma
{
	public sealed class CharacterDatabaseContext : DbContext
	{
		/// <summary>
		/// Set of characters in the database.
		/// </summary>
		public DbSet<CharacterDatabaseModel> NameEntries { get; set; }

		public CharacterDatabaseContext(DbContextOptions<CharacterDatabaseContext> options) 
			: base(options)
		{

		}

		public CharacterDatabaseContext()
		{

		}

#if DEBUG || DEBUGBUILD
		/// <inheritdoc />
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySql("Server=localhost;Database=Boomacharacters;Uid=root;Pwd=test;");
		}
#endif
	}
}
