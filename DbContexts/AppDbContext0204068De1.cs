using Microsoft.EntityFrameworkCore;
using NguyenVietTuanAnh0204068De1.Entities;

namespace NguyenVietTuanAnh0204068De1.DbContexts
{
    public class AppDbContext0204068De1 : DbContext
    {
        public AppDbContext0204068De1(DbContextOptions<AppDbContext0204068De1> options)
            : base(options)
        {
        }

        public DbSet<DoanhNghiep0204068De1> DoanhNghieps { get; set; }
        public DbSet<SanPham0204068De1> SanPhams { get; set; }
        public DbSet<DoanhNghiepSanPham0204068De1> DoanhNghiepSanPhams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DoanhNghiep0204068De1>(entity =>
            {
                entity.ToTable("DoanhNghieps");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.TenDoanhNghiep).IsRequired().HasMaxLength(200);
                entity.HasIndex(e => e.TenDoanhNghiep).IsUnique().HasDatabaseName("UQ_DoanhNghieps_TenDoanhNghiep");
                entity.Property(e => e.MaSoThue).IsRequired().HasMaxLength(20);
                entity.HasIndex(e => e.MaSoThue).IsUnique().HasDatabaseName("UQ_DoanhNghieps_MaSoThue");
                entity.Property(e => e.DiaChi).HasMaxLength(500);
            });

            modelBuilder.Entity<SanPham0204068De1>(entity =>
            {
                entity.ToTable("SanPhams");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.TenSanPham).IsRequired().HasMaxLength(200);
                entity.HasIndex(p => p.TenSanPham).IsUnique().HasDatabaseName("UQ_SanPhams_TenSanPham");
                entity.Property(p => p.MaSanPham).IsRequired().HasMaxLength(50);
                entity.HasIndex(p => p.MaSanPham).IsUnique().HasDatabaseName("UQ_SanPhams_MaSanPham");
                entity.Property(p => p.NgayNhap).IsRequired();
            });

            modelBuilder.Entity<DoanhNghiepSanPham0204068De1>(entity =>
            {
                entity.ToTable("DoanhNghiepSanPhams");
                entity.HasKey(ep => new { ep.DoanhNghiepId, ep.SanPhamId });

                entity.HasOne(ep => ep.DoanhNghiep)
                    .WithMany(e => e.DoanhNghiepSanPhams)
                    .HasForeignKey(ep => ep.DoanhNghiepId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ep => ep.SanPham)
                    .WithMany(p => p.DoanhNghiepSanPhams)
                    .HasForeignKey(ep => ep.SanPhamId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(ep => ep.SoLuong).IsRequired().HasDefaultValue(0);
            });
        }
    }
}