using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Assignment.Models
{
    public partial class MoviesContext : DbContext
    {
        public MoviesContext()
        {
        }

        public MoviesContext(DbContextOptions<MoviesContext> options)
            : base(options)
        {
        }
        public virtual DbSet<LocalUser> LocalUsers { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<MovieReview> MovieReviews { get; set; } = null!;
        public virtual DbSet<MovieReview1> MovieReviews1 { get; set; } = null!;
        public virtual DbSet<ShowTime> ShowTimes { get; set; } = null!;
        public virtual DbSet<Theater> Theaters { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-3O3I9B2;Initial Catalog=Movies;User ID=dharinee;Password=12345;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.Property(e => e.BookingId).ValueGeneratedNever();

                entity.Property(e => e.NumberOfStickers).HasColumnName("Number_of_stickers");

                entity.Property(e => e.UserId).HasMaxLength(100);

                entity.HasOne(d => d.ShowTime)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.ShowTimeId)
                    .HasConstraintName("FK__Booking__ShowTim__44FF419A");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movie");

                entity.Property(e => e.MovieId).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Genre).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(100);
            });

            modelBuilder.Entity<MovieReview>(entity =>
            {
                entity.HasKey(e => e.ReviewId)
                    .HasName("PK__MovieRev__74BC79CED0F6341F");

                entity.ToTable("MovieReview");

                entity.Property(e => e.ReviewId).ValueGeneratedNever();

                entity.Property(e => e.ReviewerName).HasMaxLength(100);
            });

            modelBuilder.Entity<MovieReview1>(entity =>
            {
                entity.HasKey(e => e.MoviereviewId);

                entity.ToTable("MovieReviews");
            });

            modelBuilder.Entity<ShowTime>(entity =>
            {
                entity.ToTable("ShowTime");

                entity.HasIndex(e => e.MovieId, "IX_ShowTime_MovieId");

                entity.HasIndex(e => e.TheaterId, "IX_ShowTime_TheaterId");

                entity.Property(e => e.ShowTimeId).ValueGeneratedNever();

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.ShowTimes)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__ShowTime__MovieI__3B75D760");

                entity.HasOne(d => d.Theater)
                    .WithMany(p => p.ShowTimes)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
                    .HasForeignKey(d => d.TheaterId)
                    .HasConstraintName("FK__ShowTime__Theate__3C69FB99");
            });

            modelBuilder.Entity<Theater>(entity =>
            {
                entity.ToTable("Theater");

                entity.Property(e => e.TheaterId).ValueGeneratedNever();

                entity.Property(e => e.Location).HasMaxLength(100);

                entity.Property(e => e.TheaterName).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
