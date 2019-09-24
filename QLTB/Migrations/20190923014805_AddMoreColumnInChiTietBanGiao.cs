using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QLTB.Migrations
{
    public partial class AddMoreColumnInChiTietBanGiao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BaoHanhDen",
                table: "ChiTietBanGiaos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "TinhTrang",
                table: "ChiTietBanGiaos",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaoHanhDen",
                table: "ChiTietBanGiaos");

            migrationBuilder.DropColumn(
                name: "TinhTrang",
                table: "ChiTietBanGiaos");
        }
    }
}
