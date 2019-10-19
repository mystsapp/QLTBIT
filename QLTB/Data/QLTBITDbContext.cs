using Microsoft.EntityFrameworkCore;
using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Data
{
    public class QLTBITDbContext : DbContext
    {
        public QLTBITDbContext(DbContextOptions<QLTBITDbContext> options) : base(options)
        {

        }
        public DbSet<LoaiThietBi> LoaiThietBis { get; set; }
        public DbSet<ThietBi> ThietBis { get; set; }
        public DbSet<BanGiao> BanGiaos { get; set; }
        public DbSet<ChiTietBanGiao> ChiTietBanGiaos { get; set; }
        public DbSet<CaiDat> CaiDats { get; set; }
        public DbSet<ChiNhanh> ChiNhanhs { get; set; }
        public DbSet<LoaiPhanMem> LoaiPhanMems { get; set; }
        public DbSet<PhanMem> PhanMems { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        //public DbSet<ThietBiCTBanGiao> ThietBiCTBanGiaos { get; set; }
        public DbSet<VanPhong> VanPhongs { get; set; }
        public DbSet<NhapKho> NhapKhos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
