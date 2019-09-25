using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QLTB.Migrations
{
    public partial class addCaiDatTableFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_CaiDats_BanGiaoId",
                table: "CaiDats",
                column: "BanGiaoId");

            migrationBuilder.CreateIndex(
                name: "IX_CaiDats_PhanMemId",
                table: "CaiDats",
                column: "PhanMemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaiDats");
        }
    }
}
