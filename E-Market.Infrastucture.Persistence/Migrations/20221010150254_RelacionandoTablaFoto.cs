using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Market.Infrastucture.Persistence.Migrations
{
    public partial class RelacionandoTablaFoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Fotos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Fotos_userId",
                table: "Fotos",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fotos_Users_userId",
                table: "Fotos",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fotos_Users_userId",
                table: "Fotos");

            migrationBuilder.DropIndex(
                name: "IX_Fotos_userId",
                table: "Fotos");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Fotos");
        }
    }
}
