using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestor_De_Pule.Migrations
{
    /// <inheritdoc />
    public partial class CaixaDateOpenClose : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateClose",
                table: "Caixas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOpen",
                table: "Caixas",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateClose",
                table: "Caixas");

            migrationBuilder.DropColumn(
                name: "DateOpen",
                table: "Caixas");
        }
    }
}
