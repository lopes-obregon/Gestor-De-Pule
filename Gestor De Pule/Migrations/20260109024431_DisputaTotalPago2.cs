using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_De_Pule.Migrations
{
    /// <inheritdoc />
    public partial class DisputaTotalPago2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalDisputa",
                table: "Disputas",
                newName: "TotalPago");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPago",
                table: "Disputas",
                newName: "TotalDisputa");
        }
    }
}
