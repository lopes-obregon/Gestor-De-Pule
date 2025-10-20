using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_De_Pule.Migrations
{
    /// <inheritdoc />
    public partial class AddApostadorPulesList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Pules_PuleId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_PuleId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "PuleId",
                table: "Animals");

            migrationBuilder.CreateTable(
                name: "AnimalPule",
                columns: table => new
                {
                    AnimaisId = table.Column<int>(type: "INTEGER", nullable: false),
                    PulesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalPule", x => new { x.AnimaisId, x.PulesId });
                    table.ForeignKey(
                        name: "FK_AnimalPule_Animals_AnimaisId",
                        column: x => x.AnimaisId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalPule_Pules_PulesId",
                        column: x => x.PulesId,
                        principalTable: "Pules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalPule_PulesId",
                table: "AnimalPule",
                column: "PulesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalPule");

            migrationBuilder.AddColumn<int>(
                name: "PuleId",
                table: "Animals",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_PuleId",
                table: "Animals",
                column: "PuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Pules_PuleId",
                table: "Animals",
                column: "PuleId",
                principalTable: "Pules",
                principalColumn: "Id");
        }
    }
}
