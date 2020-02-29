﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SuncoastRecords.Models;

namespace SuncoastRecords.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200229174716_AddedMusiciansTables")]
    partial class AddedMusiciansTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("SuncoastRecords.Models.Album", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BandID")
                        .HasColumnType("integer");

                    b.Property<bool>("IsExplicit")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("BandID");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("SuncoastRecords.Models.Band", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ContactPerson")
                        .HasColumnType("text");

                    b.Property<string>("ContactPhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("CountryOfOrigin")
                        .HasColumnType("text");

                    b.Property<bool>("IsSigned")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("NumberOfMembers")
                        .HasColumnType("integer");

                    b.Property<string>("Website")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Bands");
                });

            modelBuilder.Entity("SuncoastRecords.Models.BandMusician", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BandID")
                        .HasColumnType("integer");

                    b.Property<int>("MusicianID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("BandID");

                    b.HasIndex("MusicianID");

                    b.ToTable("BandMusicians");
                });

            modelBuilder.Entity("SuncoastRecords.Models.BandStyle", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BandID")
                        .HasColumnType("integer");

                    b.Property<string>("Style")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("BandID");

                    b.ToTable("BandStyles");
                });

            modelBuilder.Entity("SuncoastRecords.Models.Musician", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Musicians");
                });

            modelBuilder.Entity("SuncoastRecords.Models.Song", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AlbumID")
                        .HasColumnType("integer");

                    b.Property<string>("Length")
                        .HasColumnType("text");

                    b.Property<string>("Lyrics")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("AlbumID");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("SuncoastRecords.Models.SongGenre", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Genre")
                        .HasColumnType("text");

                    b.Property<int>("SongID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("SongID");

                    b.ToTable("SongGenres");
                });

            modelBuilder.Entity("SuncoastRecords.Models.Album", b =>
                {
                    b.HasOne("SuncoastRecords.Models.Band", null)
                        .WithMany("Albums")
                        .HasForeignKey("BandID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SuncoastRecords.Models.BandMusician", b =>
                {
                    b.HasOne("SuncoastRecords.Models.Band", null)
                        .WithMany("BandMusicians")
                        .HasForeignKey("BandID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuncoastRecords.Models.Musician", null)
                        .WithMany("BandMusicians")
                        .HasForeignKey("MusicianID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SuncoastRecords.Models.BandStyle", b =>
                {
                    b.HasOne("SuncoastRecords.Models.Band", null)
                        .WithMany("BandStyles")
                        .HasForeignKey("BandID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SuncoastRecords.Models.Song", b =>
                {
                    b.HasOne("SuncoastRecords.Models.Album", null)
                        .WithMany("Songs")
                        .HasForeignKey("AlbumID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SuncoastRecords.Models.SongGenre", b =>
                {
                    b.HasOne("SuncoastRecords.Models.Song", null)
                        .WithMany("SongGenres")
                        .HasForeignKey("SongID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
