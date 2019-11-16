using Microsoft.EntityFrameworkCore.Migrations;

namespace QLTB.Migrations
{
    public partial class extendIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChiNhanhId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Khoi",
                table: "AspNetUsers",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhongBan",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TinhTrang",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VanPhong",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ChiNhanhId",
                table: "AspNetUsers",
                column: "ChiNhanhId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ChiNhanhs_ChiNhanhId",
                table: "AspNetUsers",
                column: "ChiNhanhId",
                principalTable: "ChiNhanhs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ChiNhanhs_ChiNhanhId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ChiNhanhId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ChiNhanhId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Khoi",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhongBan",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TinhTrang",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VanPhong",
                table: "AspNetUsers");
        }
    }
}
