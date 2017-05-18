using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Booma.GameServerList.Lib;
using Booma.ServerSelection.Common;

namespace Booma.GameServerListService.MigrationsGenerator.Migrations
{
    [DbContext(typeof(GameServerListDbContext))]
    [Migration("20170518025159_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Booma.GameServerList.Lib.GameServerDetailsModel", b =>
                {
                    b.Property<int>("GameServerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasColumnName("address");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<int>("Region")
                        .HasColumnName("region");

                    b.Property<int>("ServerPort")
                        .HasColumnName("port");

                    b.Property<int>("Status")
                        .HasColumnName("status_flags");

                    b.HasKey("GameServerId");

                    b.ToTable("shiplist");
                });
        }
    }
}
