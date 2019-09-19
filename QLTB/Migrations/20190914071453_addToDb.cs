using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QLTB.Migrations
{
    public partial class addToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChiNhanhs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaChiNhanh = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    TenCN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DienThoai = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiNhanhs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiPhanMems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenLoaiPM = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiPhanMems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiThietBis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenLoai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiThietBis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BanGiaos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NguoiLap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nvarchar50 = table.Column<string>(name: "nvarchar(50)", maxLength: 50, nullable: true),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<string>(nullable: true),
                    NgaySua = table.Column<DateTime>(nullable: false),
                    ChiNhanhId = table.Column<int>(nullable: false),
                    VanPhong = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Khoi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    PhongBan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TinhTrang = table.Column<bool>(nullable: false),
                    LyDo = table.Column<string>(type: "nvarchar(50)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BanGiaos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BanGiaos_ChiNhanhs_ChiNhanhId",
                        column: x => x.ChiNhanhId,
                        principalTable: "ChiNhanhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NhanViens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhongBan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Khoi = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    ChiNhanhId = table.Column<int>(nullable: false),
                    nvarchar50 = table.Column<string>(name: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanViens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NhanViens_ChiNhanhs_ChiNhanhId",
                        column: x => x.ChiNhanhId,
                        principalTable: "ChiNhanhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VanPhongs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenVP = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DienThoai = table.Column<string>(maxLength: 15, nullable: true),
                    ChiNhanhId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VanPhongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VanPhongs_ChiNhanhs_ChiNhanhId",
                        column: x => x.ChiNhanhId,
                        principalTable: "ChiNhanhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhanMems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenPM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LoaiPMId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanMems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhanMems_LoaiPhanMems_LoaiPMId",
                        column: x => x.LoaiPMId,
                        principalTable: "LoaiPhanMems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThietBis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenThietBi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gia = table.Column<double>(nullable: false),
                    TrangThai = table.Column<bool>(nullable: false),
                    DienGiai = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BaoHanh = table.Column<int>(nullable: false),
                    HinhAnh = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    LoaiThietBiId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThietBis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThietBis_LoaiThietBis_LoaiThietBiId",
                        column: x => x.LoaiThietBiId,
                        principalTable: "LoaiThietBis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietBanGiaos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BanGiaoId = table.Column<int>(nullable: false),
                    ThietBiId = table.Column<int>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    DonGia = table.Column<double>(nullable: false),
                    DienGiai = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NgayGiao = table.Column<DateTime>(nullable: false),
                    NguoiNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NgayNhap = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietBanGiaos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietBanGiaos_BanGiaos_BanGiaoId",
                        column: x => x.BanGiaoId,
                        principalTable: "BanGiaos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietBanGiaos_ThietBis_ThietBiId",
                        column: x => x.ThietBiId,
                        principalTable: "ThietBis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BanGiaos_ChiNhanhId",
                table: "BanGiaos",
                column: "ChiNhanhId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBanGiaos_BanGiaoId",
                table: "ChiTietBanGiaos",
                column: "BanGiaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBanGiaos_ThietBiId",
                table: "ChiTietBanGiaos",
                column: "ThietBiId");

            migrationBuilder.CreateIndex(
                name: "IX_NhanViens_ChiNhanhId",
                table: "NhanViens",
                column: "ChiNhanhId");

            migrationBuilder.CreateIndex(
                name: "IX_PhanMems_LoaiPMId",
                table: "PhanMems",
                column: "LoaiPMId");

            migrationBuilder.CreateIndex(
                name: "IX_ThietBis_LoaiThietBiId",
                table: "ThietBis",
                column: "LoaiThietBiId");

            migrationBuilder.CreateIndex(
                name: "IX_VanPhongs_ChiNhanhId",
                table: "VanPhongs",
                column: "ChiNhanhId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietBanGiaos");

            migrationBuilder.DropTable(
                name: "NhanViens");

            migrationBuilder.DropTable(
                name: "PhanMems");

            migrationBuilder.DropTable(
                name: "VanPhongs");

            migrationBuilder.DropTable(
                name: "BanGiaos");

            migrationBuilder.DropTable(
                name: "ThietBis");

            migrationBuilder.DropTable(
                name: "LoaiPhanMems");

            migrationBuilder.DropTable(
                name: "ChiNhanhs");

            migrationBuilder.DropTable(
                name: "LoaiThietBis");
        }
    }
}
