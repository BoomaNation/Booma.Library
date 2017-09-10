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
		public DbSet<CharacterDatabaseModel> Characters { get; set; }

		/// <summary>
		/// Set of character appearances in the database.
		/// </summary>
		public DbSet<CharacterAppearanceModel> CharacterAppearances { get; set; }

		public CharacterDatabaseContext(DbContextOptions<CharacterDatabaseContext> options) 
			: base(options)
		{

		}

		public CharacterDatabaseContext()
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CharacterDatabaseModel>()
				.HasAlternateKey(c => c.CharacterName);
		}

#if DEBUG || DEBUGBUILD
		/// <inheritdoc />
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySql("Server=localhost;Database=Boomagameserver;Uid=root;Pwd=test;");
		}
#endif
	}
}
