using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_De_Pule.Migrations
{
    /// <inheritdoc />
    public partial class PuleAddRodadaIdNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pules_Rodas_RodadaId",
                table: "Pules");

            migrationBuilder.AlterColumn<int>(
                name: "RodadaId",
                table: "Pules",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Pules_Rodas_RodadaId",
                table: "Pules",
                column: "RodadaId",
                principalTable: "Rodas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pules_Rodas_RodadaId",
                table: "Pules");

            migrationBuilder.AlterColumn<int>(
                name: "RodadaId",
                table: "Pules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pules_Rodas_RodadaId",
                table: "Pules",
                column: "RodadaId",
                principalTable: "Rodas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
