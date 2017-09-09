using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Booma;

namespace Booma
{
    [DbContext(typeof(GameSessionDatabaseContext))]
    [Migration("20170828035044_GameSessionInitialMigration")]
    partial class GameSessionInitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("GaiaOnline.GameSessionModel", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<DateTime>("SessionCreationTime")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<Guid>("SessionGuid");

                    b.Property<string>("SessionIp")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("GameSessions");
                });

            modelBuilder.Entity("GaiaOnline.GameSessionModel", b =>
                {
                    b.HasOne("GaiaOnline.GaiaNameEntryModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
