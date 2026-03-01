using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBlockTypeSchemaToJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<JsonDocument>(
                name: "Schema",
                table: "BlockTypes",
                type: "json",
                nullable: false,
                oldClrType: typeof(JsonDocument),
                oldType: "jsonb");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<JsonDocument>(
                name: "Schema",
                table: "BlockTypes",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(JsonDocument),
                oldType: "json");
        }
    }
}
