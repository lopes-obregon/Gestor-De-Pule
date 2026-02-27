using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_De_Pule.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirNomeDisputaId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rodas_Disputas_DisputaId",
                table: "Rodas");

            migrationBuilder.DropColumn(
                name: "IdDisputa",
                table: "Rodas");

            migrationBuilder.AlterColumn<int>(
                name: "DisputaId",
                table: "Rodas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rodas_Disputas_DisputaId",
                table: "Rodas",
                column: "DisputaId",
                principalTable: "Disputas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rodas_Disputas_DisputaId",
                table: "Rodas");

            migrationBuilder.AlterColumn<int>(
                name: "DisputaId",
                table: "Rodas",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "IdDisputa",
                table: "Rodas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Rodas_Disputas_DisputaId",
                table: "Rodas",
                column: "DisputaId",
                principalTable: "Disputas",
                principalColumn: "Id");
        }
    }
}
