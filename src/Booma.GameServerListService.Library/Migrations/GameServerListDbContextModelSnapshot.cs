using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Booma.GameServerList.Lib;

namespace Booma.GameServerList.Lib.Migrations
{
    [DbContext(typeof(GameServerListDbContext))]
    partial class GameServerListDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Booma.GameServerList.Lib.GameServerDetailsModel", b =>
                {
                    b.Property<int>("GameServerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Name");

                    b.Property<int>("Region");

                    b.Property<int>("ServerPort");

                    b.Property<int>("Status");

                    b.HasKey("GameServerId");

                    b.ToTable("GameServers");
                });
        }
    }
}
