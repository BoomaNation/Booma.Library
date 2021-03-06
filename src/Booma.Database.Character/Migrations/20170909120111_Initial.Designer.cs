﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Booma;

namespace Booma.Database.Character.Migrations
{
    [DbContext(typeof(CharacterDatabaseContext))]
    [Migration("20170909120111_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("Booma.CharacterAppearanceModel", b =>
                {
                    b.Property<int>("CharacterId");

                    b.Property<int>("CharacterClass")
                        .HasColumnName("class");

                    b.Property<int>("SectionId");

                    b.HasKey("CharacterId");

                    b.ToTable("character_appearance");
                });

            modelBuilder.Entity("Booma.CharacterDatabaseModel", b =>
                {
                    b.Property<int>("CharacterId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<string>("CharacterName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("CreationIp")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<bool>("isLoggedIn");

                    b.HasKey("CharacterId");

                    b.ToTable("characters");
                });

            modelBuilder.Entity("Booma.CharacterAppearanceModel", b =>
                {
                    b.HasOne("Booma.CharacterDatabaseModel", "CharacterEntry")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
