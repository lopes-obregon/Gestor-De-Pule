using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_De_Pule.Migrations
{
    /// <inheritdoc />
    public partial class PuleDisputaAndDisputaPule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisputaId",
                table: "Pules",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pules_DisputaId",
                table: "Pules",
                column: "DisputaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pules_Disputas_DisputaId",
                table: "Pules",
                column: "DisputaId",
                principalTable: "Disputas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pules_Disputas_DisputaId",
                table: "Pules");

            migrationBuilder.DropIndex(
                name: "IX_Pules_DisputaId",
                table: "Pules");

            migrationBuilder.DropColumn(
                name: "DisputaId",
                table: "Pules");
        }
    }
}
