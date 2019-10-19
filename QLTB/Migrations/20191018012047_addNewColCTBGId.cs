using Microsoft.EntityFrameworkCore.Migrations;

namespace QLTB.Migrations
{
    public partial class addNewColCTBGId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CTBGId",
                table: "NhapKhos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NhapKhos_CTBGId",
                table: "NhapKhos",
                column: "CTBGId");

            migrationBuilder.AddForeignKey(
                name: "FK_NhapKhos_ChiTietBanGiaos_CTBGId",
                table: "NhapKhos",
                column: "CTBGId",
                principalTable: "ChiTietBanGiaos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhapKhos_ChiTietBanGiaos_CTBGId",
                table: "NhapKhos");

            migrationBuilder.DropIndex(
                name: "IX_NhapKhos_CTBGId",
                table: "NhapKhos");

            migrationBuilder.DropColumn(
                name: "CTBGId",
                table: "NhapKhos");
        }
    }
}
