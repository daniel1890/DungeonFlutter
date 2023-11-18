﻿// <auto-generated />
using DungeonFlutterAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DungeonFlutterAPI.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20231118205909_fix_player_again")]
    partial class fix_player_again
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("DungeonFlutterAPI.Models.Domain.HighScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("HighScores");
                });

            modelBuilder.Entity("DungeonFlutterAPI.Models.Domain.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("DungeonFlutterAPI.Models.Domain.HighScore", b =>
                {
                    b.HasOne("DungeonFlutterAPI.Models.Domain.Player", "Player")
                        .WithMany("HighScores")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("DungeonFlutterAPI.Models.Domain.Player", b =>
                {
                    b.Navigation("HighScores");
                });
#pragma warning restore 612, 618
        }
    }
}
