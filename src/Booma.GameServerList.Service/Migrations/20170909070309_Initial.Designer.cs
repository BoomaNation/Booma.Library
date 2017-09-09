using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Booma;
using HaloLive.Network.Common;

namespace Booma.GameServerList.Service.Migrations
{
    [DbContext(typeof(GameServerListDbContext))]
    [Migration("20170909070309_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("Booma.GameServerListEntryModel", b =>
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
