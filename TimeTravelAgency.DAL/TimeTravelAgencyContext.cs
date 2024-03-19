using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TimeTravelAgency.Domain.Entity;

namespace TimeTravelAgency.DAL
{
    public partial class TimeTravelAgencyContext : DbContext
    {
        public TimeTravelAgencyContext()
        {
        }

        public TimeTravelAgencyContext(DbContextOptions<TimeTravelAgencyContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Tour> Tours { get; set; } = null!;
        public virtual DbSet<Uprofile> Uprofiles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Connection string");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.DateCreate).HasColumnType("datetime");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK__Orders__TourId__4BAC3F29");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Orders__UserId__4CA06362");
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.Property(e => e.DateEnd).HasColumnType("date");

                entity.Property(e => e.DateStart).HasColumnType("date");

                entity.Property(e => e.Descriptions).HasMaxLength(1000);

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.Property(e => e.TypeTour).HasMaxLength(100);
            });

            modelBuilder.Entity<Uprofile>(entity =>
            {
                entity.ToTable("UProfile");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Phone).HasMaxLength(100);

                entity.Property(e => e.Uaddress)
                    .HasMaxLength(500)
                    .HasColumnName("UAddress");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.UProfile)
                    .HasForeignKey<Uprofile>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UProfile_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.HashPassword).HasMaxLength(500);

                entity.Property(e => e.ULogin)
                    .HasMaxLength(100)
                    .HasColumnName("ULogin");

                entity.Property(e => e.URole)
                    .HasMaxLength(100)
                    .HasColumnName("URole");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
