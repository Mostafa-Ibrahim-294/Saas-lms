using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStudentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_TeachingLevels_TeachingLevelId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ParentName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ParentPhone",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "TeachingLevelId",
                table: "Students",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Grade",
                table: "Students",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_TeachingLevels_TeachingLevelId",
                table: "Students",
                column: "TeachingLevelId",
                principalTable: "TeachingLevels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_TeachingLevels_TeachingLevelId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "TeachingLevelId",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Grade",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentName",
                table: "Students",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParentPhone",
                table: "Students",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_TeachingLevels_TeachingLevelId",
                table: "Students",
                column: "TeachingLevelId",
                principalTable: "TeachingLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
