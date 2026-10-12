using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Cau2_QuanCafe.Models;

public partial class QuanCaPheContext : DbContext
{
    public QuanCaPheContext()
    {
    }

    public QuanCaPheContext(DbContextOptions<QuanCaPheContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<Mon> Mons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=QuanCaPhe;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Vietnamese_CI_AS");

        modelBuilder.Entity<ChiTietHoaDon>(entity =>
        {
            entity.HasKey(e => e.MaCt);

            entity.ToTable("ChiTietHoaDon");

            entity.Property(e => e.MaCt).HasColumnName("MaCT");
            entity.Property(e => e.DonGia).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.MaHd).HasColumnName("MaHD");

            entity.HasOne(d => d.MaHdNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.MaHd)
                .HasConstraintName("FK_ChiTietHoaDon_HoaDon");

            entity.HasOne(d => d.MaMonNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.MaMon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietHoaDon_Mon");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHd);

            entity.ToTable("HoaDon");

            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.GhiChu).HasMaxLength(200);
            entity.Property(e => e.HinhThucTt)
                .HasMaxLength(20)
                .HasColumnName("HinhThucTT");
            entity.Property(e => e.NgayLap).HasColumnType("datetime");
        });

        modelBuilder.Entity<Mon>(entity =>
        {
            entity.HasKey(e => e.MaMon);

            entity.ToTable("Mon");

            entity.Property(e => e.DonGia).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.NhomMon).HasMaxLength(30);
            entity.Property(e => e.TenMon).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
