﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QLTB.Data;

namespace QLTB.Migrations
{
    [DbContext(typeof(QLTBITDbContext))]
    [Migration("20190923073839_ChangeTenThietBiToName")]
    partial class ChangeTenThietBiToName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QLTB.Data.Models.BanGiao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChiNhanhId");

                    b.Property<string>("Khoi")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LoaiThietBi")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LyDo")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(250);

                    b.Property<DateTime>("NgaySua");

                    b.Property<DateTime>("NgayTao");

                    b.Property<string>("NguoiLap")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NguoiNhan")
                        .IsRequired()
                        .HasColumnName("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NguoiSua");

                    b.Property<string>("PhongBan")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("TinhTrang");

                    b.Property<string>("VanPhong")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("ChiNhanhId");

                    b.ToTable("BanGiaos");
                });

            modelBuilder.Entity("QLTB.Data.Models.ChiNhanh", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("DienThoai")
                        .HasMaxLength(20);

                    b.Property<string>("MaChiNhanh")
                        .IsRequired()
                        .HasColumnType("varchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("ChiNhanhs");
                });

            modelBuilder.Entity("QLTB.Data.Models.ChiTietBanGiao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BanGiaoId");

                    b.Property<DateTime>("BaoHanhDen");

                    b.Property<string>("DienGiai")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<double>("DonGia");

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime>("NgayGiao");

                    b.Property<DateTime>("NgayNhap");

                    b.Property<string>("NguoiNhap")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("SoLuong");

                    b.Property<int>("ThietBiId");

                    b.Property<bool>("TinhTrang");

                    b.HasKey("Id");

                    b.HasIndex("BanGiaoId");

                    b.HasIndex("ThietBiId");

                    b.ToTable("ChiTietBanGiaos");
                });

            modelBuilder.Entity("QLTB.Data.Models.LoaiPhanMem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("LoaiPhanMems");
                });

            modelBuilder.Entity("QLTB.Data.Models.LoaiThietBi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("LoaiThietBis");
                });

            modelBuilder.Entity("QLTB.Data.Models.NhanVien", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChiNhanhId");

                    b.Property<string>("Khoi")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PhongBan")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("VanPhong")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("ChiNhanhId");

                    b.ToTable("NhanViens");
                });

            modelBuilder.Entity("QLTB.Data.Models.PhanMem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LoaiPMId");

                    b.Property<string>("TenPM")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("LoaiPMId");

                    b.ToTable("PhanMems");
                });

            modelBuilder.Entity("QLTB.Data.Models.ThietBi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BaoHanh");

                    b.Property<string>("DienGiai")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<double>("Gia");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("LoaiThietBiId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("TrangThai");

                    b.HasKey("Id");

                    b.HasIndex("LoaiThietBiId");

                    b.ToTable("ThietBis");
                });

            modelBuilder.Entity("QLTB.Data.Models.VanPhong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChiNhanhId");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("DienThoai")
                        .HasMaxLength(15);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("ChiNhanhId");

                    b.ToTable("VanPhongs");
                });

            modelBuilder.Entity("QLTB.Data.Models.BanGiao", b =>
                {
                    b.HasOne("QLTB.Data.Models.ChiNhanh", "ChiNhanh")
                        .WithMany()
                        .HasForeignKey("ChiNhanhId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QLTB.Data.Models.ChiTietBanGiao", b =>
                {
                    b.HasOne("QLTB.Data.Models.BanGiao", "BanGiao")
                        .WithMany()
                        .HasForeignKey("BanGiaoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QLTB.Data.Models.ThietBi", "ThietBi")
                        .WithMany()
                        .HasForeignKey("ThietBiId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QLTB.Data.Models.NhanVien", b =>
                {
                    b.HasOne("QLTB.Data.Models.ChiNhanh", "ChiNhanh")
                        .WithMany()
                        .HasForeignKey("ChiNhanhId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QLTB.Data.Models.PhanMem", b =>
                {
                    b.HasOne("QLTB.Data.Models.LoaiPhanMem", "LoaiPhanMem")
                        .WithMany()
                        .HasForeignKey("LoaiPMId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QLTB.Data.Models.ThietBi", b =>
                {
                    b.HasOne("QLTB.Data.Models.LoaiThietBi", "LoaiThietBi")
                        .WithMany()
                        .HasForeignKey("LoaiThietBiId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QLTB.Data.Models.VanPhong", b =>
                {
                    b.HasOne("QLTB.Data.Models.ChiNhanh", "ChiNhanh")
                        .WithMany()
                        .HasForeignKey("ChiNhanhId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
