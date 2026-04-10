using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateScheduleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Schedules",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(7)",
                oldUnicode: false,
                oldMaxLength: 7);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Schedules",
                type: "character varying(7)",
                unicode: false,
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
