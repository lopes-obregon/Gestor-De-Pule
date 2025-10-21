using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_De_Pule.Migrations
{
    /// <inheritdoc />
    public partial class AddValorPule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pules_Apostadors_ApostadorId",
                table: "Pules");

            migrationBuilder.AlterColumn<int>(
                name: "ApostadorId",
                table: "Pules",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<float>(
                name: "Valor",
                table: "Pules",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddForeignKey(
                name: "FK_Pules_Apostadors_ApostadorId",
                table: "Pules",
                column: "ApostadorId",
                principalTable: "Apostadors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pules_Apostadors_ApostadorId",
                table: "Pules");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Pules");

            migrationBuilder.AlterColumn<int>(
                name: "ApostadorId",
                table: "Pules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pules_Apostadors_ApostadorId",
                table: "Pules",
                column: "ApostadorId",
                principalTable: "Apostadors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
