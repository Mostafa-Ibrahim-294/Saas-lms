using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editques : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reuse",
                table: "ModuleItems");

            migrationBuilder.AddColumn<int>(
                name: "Reuse",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reuse",
                table: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "Reuse",
                table: "ModuleItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
