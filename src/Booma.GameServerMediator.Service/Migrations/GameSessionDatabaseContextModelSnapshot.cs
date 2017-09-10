using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Booma;

namespace Booma.GameServerMediator.Service.Migrations
{
    [DbContext(typeof(GameSessionDatabaseContext))]
    partial class GameSessionDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("Booma.GameSessionModel", b =>
                {
                    b.Property<int>("CharacterId");

                    b.Property<DateTime>("SessionCreationTime")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<Guid>("SessionGuid");

                    b.Property<string>("SessionIp")
                        .HasMaxLength(15);

                    b.Property<bool>("isSessionClaimed")
                        .HasColumnName("IsClaimed");

                    b.HasKey("CharacterId");

                    b.ToTable("character_sessions");
                });

            modelBuilder.Entity("Booma.GameSessionModel", b =>
                {
                    b.HasOne("Booma.CharacterDatabaseModel", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
