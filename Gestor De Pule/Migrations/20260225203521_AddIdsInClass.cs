using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_De_Pule.Migrations
{
    /// <inheritdoc />
    public partial class AddIdsInClass : Migration
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

            migrationBuilder.RenameColumn(
                name: "DisputaId",
                table: "Pules",
                newName: "DisputaId1");

            migrationBuilder.RenameColumn(
                name: "ApostadorId",
                table: "Pules",
                newName: "ApostadorId1");

            migrationBuilder.RenameIndex(
                name: "IX_Pules_DisputaId",
                table: "Pules",
                newName: "IX_Pules_DisputaId1");

            migrationBuilder.RenameIndex(
                name: "IX_Pules_ApostadorId",
                table: "Pules",
                newName: "IX_Pules_ApostadorId1");

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "Pules",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AddForeignKey(
                name: "FK_Pules_Apostadors_ApostadorId1",
                table: "Pules",
                column: "ApostadorId1",
                principalTable: "Apostadors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pules_Disputas_DisputaId1",
                table: "Pules",
                column: "DisputaId1",
                principalTable: "Disputas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pules_Apostadors_ApostadorId1",
                table: "Pules");

            migrationBuilder.DropForeignKey(
                name: "FK_Pules_Disputas_DisputaId1",
                table: "Pules");

            migrationBuilder.RenameColumn(
                name: "DisputaId1",
                table: "Pules",
                newName: "DisputaId");

            migrationBuilder.RenameColumn(
                name: "ApostadorId1",
                table: "Pules",
                newName: "ApostadorId");

            migrationBuilder.RenameIndex(
                name: "IX_Pules_DisputaId1",
                table: "Pules",
                newName: "IX_Pules_DisputaId");

            migrationBuilder.RenameIndex(
                name: "IX_Pules_ApostadorId1",
                table: "Pules",
                newName: "IX_Pules_ApostadorId");

            migrationBuilder.AlterColumn<float>(
                name: "Valor",
                table: "Pules",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

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
    }
}
