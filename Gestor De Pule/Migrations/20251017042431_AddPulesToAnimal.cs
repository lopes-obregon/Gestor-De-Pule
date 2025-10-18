using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_De_Pule.Migrations
{
    /// <inheritdoc />
    public partial class AddPulesToAnimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apostadors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Contato = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apostadors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApostadorId = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusPagamento = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pules_Apostadors_ApostadorId",
                        column: x => x.ApostadorId,
                        principalTable: "Apostadors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Proprietário = table.Column<string>(type: "TEXT", nullable: false),
                    Treinador = table.Column<string>(type: "TEXT", nullable: false),
                    Jockey = table.Column<string>(type: "TEXT", nullable: false),
                    Cidade = table.Column<string>(type: "TEXT", nullable: false),
                    Número = table.Column<int>(type: "INTEGER", nullable: false),
                    PuleId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animals_Pules_PuleId",
                        column: x => x.PuleId,
                        principalTable: "Pules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_PuleId",
                table: "Animals",
                column: "PuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Pules_ApostadorId",
                table: "Pules",
                column: "ApostadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Pules");

            migrationBuilder.DropTable(
                name: "Apostadors");
        }
    }
}
