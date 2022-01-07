using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Prometheus.DB.Entities;

#nullable disable

namespace Prometheus.DB.Entities.DataContext
{
    public partial class PrometheusContext : DbContext
    {
        public PrometheusContext()
        {
        }

        public PrometheusContext(DbContextOptions<PrometheusContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Apartment> Apartment { get; set; }
        public virtual DbSet<Bill> Bill { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=Prometheus;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Idate)
                    .HasColumnType("datetime")
                    .HasColumnName("IDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.PlateNo).HasMaxLength(9);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Tc)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("TC");

                entity.Property(e => e.Udate)
                    .HasColumnType("datetime")
                    .HasColumnName("UDate");

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.Admin)
                    .HasForeignKey(d => d.ApartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Admin_Apartment");
            });

            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.Property(e => e.ApartmentType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BlockName)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.Property(e => e.BillType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Idate)
                    .HasColumnType("datetime")
                    .HasColumnName("IDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Udate)
                    .HasColumnType("datetime")
                    .HasColumnName("UDate");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bill_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Idate)
                    .HasColumnType("datetime")
                    .HasColumnName("IDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(11);

                entity.Property(e => e.PlateNo).HasMaxLength(9);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Tc)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("TC");

                entity.Property(e => e.Udate)
                    .HasColumnType("datetime")
                    .HasColumnName("UDate");

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.ApartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Apartment");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
