using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_De_Pule.Migrations
{
    /// <inheritdoc />
    public partial class AddIdsPule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pules_Apostadors_ApostadorId1",
                table: "Pules");

            migrationBuilder.DropForeignKey(
                name: "FK_Pules_Disputas_DisputaId1",
                table: "Pules");

            migrationBuilder.DropIndex(
                name: "IX_Pules_ApostadorId1",
                table: "Pules");

            migrationBuilder.DropIndex(
                name: "IX_Pules_DisputaId1",
                table: "Pules");

            migrationBuilder.DropColumn(
                name: "ApostadorId1",
                table: "Pules");

            migrationBuilder.DropColumn(
                name: "DisputaId1",
                table: "Pules");

            migrationBuilder.AddColumn<int>(
                name: "ApostadorId",
                table: "Pules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DisputaId",
                table: "Pules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pules_ApostadorId",
                table: "Pules",
                column: "ApostadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pules_DisputaId",
                table: "Pules",
                column: "DisputaId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pules_Apostadors_ApostadorId",
                table: "Pules");

            migrationBuilder.DropForeignKey(
                name: "FK_Pules_Disputas_DisputaId",
                table: "Pules");

            migrationBuilder.DropIndex(
                name: "IX_Pules_ApostadorId",
                table: "Pules");

            migrationBuilder.DropIndex(
                name: "IX_Pules_DisputaId",
                table: "Pules");

            migrationBuilder.DropColumn(
                name: "ApostadorId",
                table: "Pules");

            migrationBuilder.DropColumn(
                name: "DisputaId",
                table: "Pules");

            migrationBuilder.AddColumn<int>(
                name: "ApostadorId1",
                table: "Pules",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DisputaId1",
                table: "Pules",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pules_ApostadorId1",
                table: "Pules",
                column: "ApostadorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Pules_DisputaId1",
                table: "Pules",
                column: "DisputaId1");

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
    }
}
