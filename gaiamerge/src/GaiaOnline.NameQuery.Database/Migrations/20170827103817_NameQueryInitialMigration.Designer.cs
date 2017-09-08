using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GaiaOnline;

namespace GaiaOnline.NameQuery.Database.Migrations
{
    [DbContext(typeof(GaiaNameQueryDatabaseContext))]
    [Migration("20170827103817_NameQueryInitialMigration")]
    partial class NameQueryInitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("GaiaOnline.GaiaNameEntryModel", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AvatarUsername")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("AvatarEntries");
                });
        }
    }
}
