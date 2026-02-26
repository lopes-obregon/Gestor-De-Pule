using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_De_Pule.Migrations
{
    /// <inheritdoc />
    public partial class AddNavegateAndIdResultadoOfTheRodada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disputas_Caixas_CaixaId",
                table: "Disputas");

            migrationBuilder.DropForeignKey(
                name: "FK_Resultados_Rodas_RodadaId",
                table: "Resultados");

            migrationBuilder.AlterColumn<int>(
                name: "RodadaId",
                table: "Resultados",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CaixaId",
                table: "Disputas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Disputas_Caixas_CaixaId",
                table: "Disputas",
                column: "CaixaId",
                principalTable: "Caixas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resultados_Rodas_RodadaId",
                table: "Resultados",
                column: "RodadaId",
                principalTable: "Rodas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disputas_Caixas_CaixaId",
                table: "Disputas");

            migrationBuilder.DropForeignKey(
                name: "FK_Resultados_Rodas_RodadaId",
                table: "Resultados");

            migrationBuilder.AlterColumn<int>(
                name: "RodadaId",
                table: "Resultados",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "CaixaId",
                table: "Disputas",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Disputas_Caixas_CaixaId",
                table: "Disputas",
                column: "CaixaId",
                principalTable: "Caixas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resultados_Rodas_RodadaId",
                table: "Resultados",
                column: "RodadaId",
                principalTable: "Rodas",
                principalColumn: "Id");
        }
    }
}
