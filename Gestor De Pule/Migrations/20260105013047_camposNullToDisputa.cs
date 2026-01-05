using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_De_Pule.Migrations
{
    /// <inheritdoc />
    public partial class camposNullToDisputa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disputas_Caixas_CaixaId",
                table: "Disputas");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disputas_Caixas_CaixaId",
                table: "Disputas");

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
        }
    }
}
