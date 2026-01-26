using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_De_Pule.Migrations
{
    /// <inheritdoc />
    public partial class Rodada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RodadaId",
                table: "Resultados",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RodadaId",
                table: "Pules",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rodas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DisputaId = table.Column<int>(type: "INTEGER", nullable: true),
                    Nrodadas = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rodas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rodas_Disputas_DisputaId",
                        column: x => x.DisputaId,
                        principalTable: "Disputas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resultados_RodadaId",
                table: "Resultados",
                column: "RodadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pules_RodadaId",
                table: "Pules",
                column: "RodadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rodas_DisputaId",
                table: "Rodas",
                column: "DisputaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pules_Rodas_RodadaId",
                table: "Pules",
                column: "RodadaId",
                principalTable: "Rodas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resultados_Rodas_RodadaId",
                table: "Resultados",
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

            migrationBuilder.DropForeignKey(
                name: "FK_Resultados_Rodas_RodadaId",
                table: "Resultados");

            migrationBuilder.DropTable(
                name: "Rodas");

            migrationBuilder.DropIndex(
                name: "IX_Resultados_RodadaId",
                table: "Resultados");

            migrationBuilder.DropIndex(
                name: "IX_Pules_RodadaId",
                table: "Pules");

            migrationBuilder.DropColumn(
                name: "RodadaId",
                table: "Resultados");

            migrationBuilder.DropColumn(
                name: "RodadaId",
                table: "Pules");
        }
    }
}
