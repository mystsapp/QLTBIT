using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QLTB.Migrations
{
    public partial class addNewDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChiNhanhs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaChiNhanh = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DienThoai = table.Column<string>(maxLength: 20, nullable: true),
                    KhuVuc = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
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
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
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
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiThietBis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhapKhos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenThietBi = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DienGiaiTB = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    NguoiNhapKho = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    NgaySuDung = table.Column<DateTime>(nullable: true),
                    NguoiSuDung = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    LyDo = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    NgayNhapKho = table.Column<DateTime>(nullable: true),
                    KhoVanPhong = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ThanhLy = table.Column<bool>(nullable: false),
                    NgayThanhLy = table.Column<DateTime>(nullable: true),
                    NguoiThanhLy = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CTBGId = table.Column<int>(nullable: false),
                    KhuVuc = table.Column<string>(type: "nvarchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhapKhos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhongBan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Khoi = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    ChiNhanhId = table.Column<int>(nullable: false),
                    VanPhong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TinhTrang = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_ChiNhanhs_ChiNhanhId",
                        column: x => x.ChiNhanhId,
                        principalTable: "ChiNhanhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BanGiaos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NguoiLap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NguoiNhan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NguoiSua = table.Column<string>(nullable: true),
                    NgaySua = table.Column<DateTime>(nullable: true),
                    ChiNhanhId = table.Column<int>(nullable: false),
                    VanPhong = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Khoi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    PhongBan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TinhTrang = table.Column<bool>(nullable: false),
                    LyDo = table.Column<string>(type: "nvarchar(50)", maxLength: 250, nullable: true),
                    LoaiThietBi = table.Column<string>(type: "nvarchar(50)", nullable: true)
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
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhongBan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Khoi = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    ChiNhanhId = table.Column<int>(nullable: false),
                    VanPhong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TinhTrang = table.Column<bool>(nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
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
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaiDats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BanGiaoId = table.Column<int>(nullable: false),
                    PhanMemId = table.Column<int>(nullable: false),
                    DonGia = table.Column<double>(nullable: false),
                    ThoiGianSuDung = table.Column<DateTime>(nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TinhTrang = table.Column<bool>(nullable: false),
                    DienGiai = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NgayGiao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaiDats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaiDats_BanGiaos_BanGiaoId",
                        column: x => x.BanGiaoId,
                        principalTable: "BanGiaos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaiDats_PhanMems_PhanMemId",
                        column: x => x.PhanMemId,
                        principalTable: "PhanMems",
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
                    BaoHanhDen = table.Column<DateTime>(nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ChuyenSuDung = table.Column<bool>(nullable: false),
                    TinhTrang = table.Column<bool>(nullable: false),
                    DienGiai = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NgayGiao = table.Column<DateTime>(nullable: true),
                    NgayChuyen = table.Column<DateTime>(nullable: true)
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ChiNhanhId",
                table: "AspNetUsers",
                column: "ChiNhanhId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BanGiaos_ChiNhanhId",
                table: "BanGiaos",
                column: "ChiNhanhId");

            migrationBuilder.CreateIndex(
                name: "IX_CaiDats_BanGiaoId",
                table: "CaiDats",
                column: "BanGiaoId");

            migrationBuilder.CreateIndex(
                name: "IX_CaiDats_PhanMemId",
                table: "CaiDats",
                column: "PhanMemId");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CaiDats");

            migrationBuilder.DropTable(
                name: "ChiTietBanGiaos");

            migrationBuilder.DropTable(
                name: "NhanViens");

            migrationBuilder.DropTable(
                name: "NhapKhos");

            migrationBuilder.DropTable(
                name: "VanPhongs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PhanMems");

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
