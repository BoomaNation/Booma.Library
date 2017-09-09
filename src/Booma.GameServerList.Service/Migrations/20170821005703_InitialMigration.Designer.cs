using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Booma;
using HaloLive.Network.Common;

namespace Booma
{
    [DbContext(typeof(GameServerListDbContext))]
    [Migration("20170821005703_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("GaiaOnline.GameServerListEntryModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EndpointAddress")
                        .IsRequired();

                    b.Property<int>("EndpointPort");

                    b.Property<int>("Region");

                    b.Property<string>("ServerName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("GameServers");
                });
        }
    }
}
