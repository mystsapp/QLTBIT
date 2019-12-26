using Microsoft.EntityFrameworkCore.Migrations;

namespace QLTB.Migrations
{
    public partial class fixKVcol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BanGiaos_ChiNhanhs_ChiNhanhId",
                table: "BanGiaos");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanViens_ChiNhanhs_ChiNhanhId",
                table: "NhanViens");

            migrationBuilder.DropColumn(
                name: "VanPhong",
                table: "NhanViens");

            migrationBuilder.DropColumn(
                name: "KhuVuc",
                table: "ChiNhanhs");

            migrationBuilder.DropColumn(
                name: "VanPhong",
                table: "BanGiaos");

            migrationBuilder.RenameColumn(
                name: "ChiNhanhId",
                table: "NhanViens",
                newName: "VanPhongId");

            migrationBuilder.RenameIndex(
                name: "IX_NhanViens_ChiNhanhId",
                table: "NhanViens",
                newName: "IX_NhanViens_VanPhongId");

            migrationBuilder.RenameColumn(
                name: "ChiNhanhId",
                table: "BanGiaos",
                newName: "VanPhongId");

            migrationBuilder.RenameIndex(
                name: "IX_BanGiaos_ChiNhanhId",
                table: "BanGiaos",
                newName: "IX_BanGiaos_VanPhongId");

            migrationBuilder.AddForeignKey(
                name: "FK_BanGiaos_VanPhongs_VanPhongId",
                table: "BanGiaos",
                column: "VanPhongId",
                principalTable: "VanPhongs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanViens_VanPhongs_VanPhongId",
                table: "NhanViens",
                column: "VanPhongId",
                principalTable: "VanPhongs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BanGiaos_VanPhongs_VanPhongId",
                table: "BanGiaos");

            migrationBuilder.DropForeignKey(
                name: "FK_NhanViens_VanPhongs_VanPhongId",
                table: "NhanViens");

            migrationBuilder.RenameColumn(
                name: "VanPhongId",
                table: "NhanViens",
                newName: "ChiNhanhId");

            migrationBuilder.RenameIndex(
                name: "IX_NhanViens_VanPhongId",
                table: "NhanViens",
                newName: "IX_NhanViens_ChiNhanhId");

            migrationBuilder.RenameColumn(
                name: "VanPhongId",
                table: "BanGiaos",
                newName: "ChiNhanhId");

            migrationBuilder.RenameIndex(
                name: "IX_BanGiaos_VanPhongId",
                table: "BanGiaos",
                newName: "IX_BanGiaos_ChiNhanhId");

            migrationBuilder.AddColumn<string>(
                name: "VanPhong",
                table: "NhanViens",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KhuVuc",
                table: "ChiNhanhs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VanPhong",
                table: "BanGiaos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_BanGiaos_ChiNhanhs_ChiNhanhId",
                table: "BanGiaos",
                column: "ChiNhanhId",
                principalTable: "ChiNhanhs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanViens_ChiNhanhs_ChiNhanhId",
                table: "NhanViens",
                column: "ChiNhanhId",
                principalTable: "ChiNhanhs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
