using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_De_Pule.Migrations
{
    /// <inheritdoc />
    public partial class pendente03012026 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resultados_Animals_AnimalId",
                table: "Resultados");

            migrationBuilder.DropForeignKey(
                name: "FK_Resultados_Disputas_DisputaId",
                table: "Resultados");

            migrationBuilder.AlterColumn<int>(
                name: "DisputaId",
                table: "Resultados",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AnimalId",
                table: "Resultados",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Resultados_Animals_AnimalId",
                table: "Resultados",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resultados_Disputas_DisputaId",
                table: "Resultados",
                column: "DisputaId",
                principalTable: "Disputas",
                principalColumn: "Id");
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

            migrationBuilder.AlterColumn<int>(
                name: "DisputaId",
                table: "Resultados",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnimalId",
                table: "Resultados",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

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
    }
}
