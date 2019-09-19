using Microsoft.EntityFrameworkCore.Migrations;

namespace QLTB.Migrations
{
    public partial class changeTenToName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenLoai",
                table: "LoaiThietBis",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TenCN",
                table: "ChiNhanhs",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "LoaiThietBis",
                newName: "TenLoai");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ChiNhanhs",
                newName: "TenCN");
        }
    }
}
