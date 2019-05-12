using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Models.Models;

namespace API.Data
{
    public partial class BackendProjContext : DbContext
    {
        public BackendProjContext()
        {
        }

        public BackendProjContext(DbContextOptions<BackendProjContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<TblFestivals> TblFestivals { get; set; }
        public virtual DbSet<TblPrice> TblPrice { get; set; }
        public virtual DbSet<TblReservation> TblReservation { get; set; }
        public virtual DbSet<TblSeat> TblSeat { get; set; }
        public virtual DbSet<TblTypeCategory> TblTypeCategory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BackendProj;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BusNumber).HasMaxLength(10);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(10);

                entity.Property(e => e.LastName).HasMaxLength(10);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.PostCode).HasMaxLength(10);

                entity.Property(e => e.Street).HasMaxLength(10);

                entity.Property(e => e.StreetNumber).HasMaxLength(10);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<TblFestivals>(entity =>
            {
                entity.HasKey(e => e.MusicEvenementId);

                entity.ToTable("tblFestivals");

                entity.Property(e => e.MusicEvenementId).ValueGeneratedNever();

                entity.Property(e => e.MusicEvenementName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TblPrice>(entity =>
            {
                entity.HasKey(e => e.Priceid);

                entity.Property(e => e.Priceid).ValueGeneratedNever();

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.MusicEvenement)
                    .WithMany(p => p.TblPrice)
                    .HasForeignKey(d => d.MusicEvenementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblPrice_tblFestivals");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.TblPrice)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblPrice_tblTypeCategory");
            });


            modelBuilder.Entity<TblReservation>(entity =>
            {
                entity.HasKey(e => e.ReservationId);

                entity.ToTable("tblReservation");

                entity.Property(e => e.ReservationId).ValueGeneratedNever();

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.MusicEvement)
                    .WithMany(p => p.TblReservation)
                    .HasForeignKey(d => d.MusicEvementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblReservation_tblFestivals");

                entity.HasOne(d => d.Price)
                    .WithMany(p => p.TblReservation)
                    .HasForeignKey(d => d.PriceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblReservation_TblPrice");

                entity.HasOne(d => d.Seat)
                    .WithMany(p => p.TblReservation)
                    .HasForeignKey(d => d.SeatId)
                    .HasConstraintName("FK_tblReservation_tblSeat");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblReservation)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblReservation_AspNetUsers");
            });

            modelBuilder.Entity<TblSeat>(entity =>
            {
                entity.HasKey(e => e.SeatId)
                    .HasName("PK_tblSeat_1");

                entity.ToTable("tblSeat");

                entity.Property(e => e.SeatId).ValueGeneratedNever();
            });

            modelBuilder.Entity<TblTypeCategory>(entity =>
            {
                entity.HasKey(e => e.Type);

                entity.ToTable("tblTypeCategory");

                entity.Property(e => e.Type).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);
            });
        }
    }
}
