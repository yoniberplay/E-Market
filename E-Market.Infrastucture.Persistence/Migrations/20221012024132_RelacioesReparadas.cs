using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Market.Infrastucture.Persistence.Migrations
{
    public partial class RelacioesReparadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fotos_Anuncios_AnuncioID",
                table: "Fotos");

            migrationBuilder.DropForeignKey(
                name: "FK_Fotos_Users_userId",
                table: "Fotos");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Fotos",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Fotos_userId",
                table: "Fotos",
                newName: "IX_Fotos_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fotos_Anuncios_AnuncioID",
                table: "Fotos",
                column: "AnuncioID",
                principalTable: "Anuncios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fotos_Users_UserId",
                table: "Fotos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fotos_Anuncios_AnuncioID",
                table: "Fotos");

            migrationBuilder.DropForeignKey(
                name: "FK_Fotos_Users_UserId",
                table: "Fotos");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Fotos",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Fotos_UserId",
                table: "Fotos",
                newName: "IX_Fotos_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fotos_Anuncios_AnuncioID",
                table: "Fotos",
                column: "AnuncioID",
                principalTable: "Anuncios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fotos_Users_userId",
                table: "Fotos",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
