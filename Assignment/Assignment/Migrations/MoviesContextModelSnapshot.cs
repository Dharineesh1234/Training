﻿// <auto-generated />
using System;
using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Assignment.Migrations
{
    [DbContext(typeof(MoviesContext))]
    partial class MoviesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Assignment.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int?>("NumberOfStickers")
                        .HasColumnType("int")
                        .HasColumnName("Number_of_stickers");

                    b.Property<int?>("ShowTimeId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("BookingId");

                    b.HasIndex("ShowTimeId");

                    b.ToTable("Booking", (string)null);
                });

            modelBuilder.Entity("Assignment.Models.LocalUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pasword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LocalUsers");
                });

            modelBuilder.Entity("Assignment.Models.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Genre")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Title")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MovieId");

                    b.ToTable("Movie", (string)null);
                });

            modelBuilder.Entity("Assignment.Models.MovieReview", b =>
                {
                    b.Property<int>("ReviewId")
                        .HasColumnType("int");

                    b.Property<int?>("MovieId")
                        .HasColumnType("int");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("ReviewText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReviewerName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ReviewId")
                        .HasName("PK__MovieRev__74BC79CED0F6341F");

                    b.ToTable("MovieReview", (string)null);
                });

            modelBuilder.Entity("Assignment.Models.MovieReview1", b =>
                {
                    b.Property<int>("MoviereviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MoviereviewId"), 1L, 1);

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("ReviewerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MoviereviewId");

                    b.ToTable("MovieReviews", (string)null);
                });

            modelBuilder.Entity("Assignment.Models.ShowTime", b =>
                {
                    b.Property<int>("ShowTimeId")
                        .HasColumnType("int");

                    b.Property<int?>("MovieId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime");

                    b.Property<int?>("TheaterId")
                        .HasColumnType("int");

                    b.HasKey("ShowTimeId");

                    b.HasIndex(new[] { "MovieId" }, "IX_ShowTime_MovieId");

                    b.HasIndex(new[] { "TheaterId" }, "IX_ShowTime_TheaterId");

                    b.ToTable("ShowTime", (string)null);
                });

            modelBuilder.Entity("Assignment.Models.Theater", b =>
                {
                    b.Property<int>("TheaterId")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TheaterName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("TheaterId");

                    b.ToTable("Theater", (string)null);
                });

            modelBuilder.Entity("Assignment.Models.Booking", b =>
                {
                    b.HasOne("Assignment.Models.ShowTime", "ShowTime")
                        .WithMany("Bookings")
                        .HasForeignKey("ShowTimeId")
                        .HasConstraintName("FK__Booking__ShowTim__44FF419A");

                    b.Navigation("ShowTime");
                });

            modelBuilder.Entity("Assignment.Models.ShowTime", b =>
                {
                    b.HasOne("Assignment.Models.Movie", "Movie")
                        .WithMany("ShowTimes")
                        .HasForeignKey("MovieId")
                        .HasConstraintName("FK__ShowTime__MovieI__3B75D760");

                    b.HasOne("Assignment.Models.Theater", "Theater")
                        .WithMany("ShowTimes")
                        .HasForeignKey("TheaterId")
                        .HasConstraintName("FK__ShowTime__Theate__3C69FB99");

                    b.Navigation("Movie");

                    b.Navigation("Theater");
                });

            modelBuilder.Entity("Assignment.Models.Movie", b =>
                {
                    b.Navigation("ShowTimes");
                });

            modelBuilder.Entity("Assignment.Models.ShowTime", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("Assignment.Models.Theater", b =>
                {
                    b.Navigation("ShowTimes");
                });
            
#pragma warning restore 612, 618
        }
    }
}
