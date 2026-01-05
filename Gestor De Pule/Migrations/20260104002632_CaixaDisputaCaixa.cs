using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_De_Pule.Migrations
{
    /// <inheritdoc />
    public partial class CaixaDisputaCaixa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CaixaId",
                table: "Disputas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Disputas_CaixaId",
                table: "Disputas",
                column: "CaixaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Disputas_Caixas_CaixaId",
                table: "Disputas",
                column: "CaixaId",
                principalTable: "Caixas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disputas_Caixas_CaixaId",
                table: "Disputas");

            migrationBuilder.DropIndex(
                name: "IX_Disputas_CaixaId",
                table: "Disputas");

            migrationBuilder.DropColumn(
                name: "CaixaId",
                table: "Disputas");
        }
    }
}
