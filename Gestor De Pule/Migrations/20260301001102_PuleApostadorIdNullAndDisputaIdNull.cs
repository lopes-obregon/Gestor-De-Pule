using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_De_Pule.Migrations
{
    /// <inheritdoc />
    public partial class PuleApostadorIdNullAndDisputaIdNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pules_Apostadors_ApostadorId",
                table: "Pules");

            migrationBuilder.DropForeignKey(
                name: "FK_Pules_Disputas_DisputaId",
                table: "Pules");

            migrationBuilder.AlterColumn<int>(
                name: "DisputaId",
                table: "Pules",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ApostadorId",
                table: "Pules",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Pules_Apostadors_ApostadorId",
                table: "Pules",
                column: "ApostadorId",
                principalTable: "Apostadors",
                principalColumn: "Id");

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
                name: "FK_Pules_Apostadors_ApostadorId",
                table: "Pules");

            migrationBuilder.DropForeignKey(
                name: "FK_Pules_Disputas_DisputaId",
                table: "Pules");

            migrationBuilder.AlterColumn<int>(
                name: "DisputaId",
                table: "Pules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Pules_Disputas_DisputaId",
                table: "Pules",
                column: "DisputaId",
                principalTable: "Disputas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
