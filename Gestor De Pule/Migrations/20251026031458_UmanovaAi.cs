using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_De_Pule.Migrations
{
    /// <inheritdoc />
    public partial class UmanovaAi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resultado_Animals_AnimalId",
                table: "Resultado");

            migrationBuilder.DropForeignKey(
                name: "FK_Resultado_Disputa_DisputaId",
                table: "Resultado");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resultado",
                table: "Resultado");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Disputa",
                table: "Disputa");

            migrationBuilder.RenameTable(
                name: "Resultado",
                newName: "Resultados");

            migrationBuilder.RenameTable(
                name: "Disputa",
                newName: "Disputas");

            migrationBuilder.RenameIndex(
                name: "IX_Resultado_DisputaId",
                table: "Resultados",
                newName: "IX_Resultados_DisputaId");

            migrationBuilder.RenameIndex(
                name: "IX_Resultado_AnimalId",
                table: "Resultados",
                newName: "IX_Resultados_AnimalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resultados",
                table: "Resultados",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Disputas",
                table: "Disputas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resultados_Animals_AnimalId",
                table: "Resultados",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resultados_Disputas_DisputaId",
                table: "Resultados",
                column: "DisputaId",
                principalTable: "Disputas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resultados_Animals_AnimalId",
                table: "Resultados");

            migrationBuilder.DropForeignKey(
                name: "FK_Resultados_Disputas_DisputaId",
                table: "Resultados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resultados",
                table: "Resultados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Disputas",
                table: "Disputas");

            migrationBuilder.RenameTable(
                name: "Resultados",
                newName: "Resultado");

            migrationBuilder.RenameTable(
                name: "Disputas",
                newName: "Disputa");

            migrationBuilder.RenameIndex(
                name: "IX_Resultados_DisputaId",
                table: "Resultado",
                newName: "IX_Resultado_DisputaId");

            migrationBuilder.RenameIndex(
                name: "IX_Resultados_AnimalId",
                table: "Resultado",
                newName: "IX_Resultado_AnimalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resultado",
                table: "Resultado",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Disputa",
                table: "Disputa",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resultado_Animals_AnimalId",
                table: "Resultado",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resultado_Disputa_DisputaId",
                table: "Resultado",
                column: "DisputaId",
                principalTable: "Disputa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
