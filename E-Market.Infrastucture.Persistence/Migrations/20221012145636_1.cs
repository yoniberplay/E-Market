using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Market.Infrastucture.Persistence.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fotos_Anuncios_AnuncioID",
                table: "Fotos");

            migrationBuilder.AddForeignKey(
                name: "FK_Fotos_Anuncios_AnuncioID",
                table: "Fotos",
                column: "AnuncioID",
                principalTable: "Anuncios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fotos_Anuncios_AnuncioID",
                table: "Fotos");

            migrationBuilder.AddForeignKey(
                name: "FK_Fotos_Anuncios_AnuncioID",
                table: "Fotos",
                column: "AnuncioID",
                principalTable: "Anuncios",
                principalColumn: "Id");
        }
    }
}
