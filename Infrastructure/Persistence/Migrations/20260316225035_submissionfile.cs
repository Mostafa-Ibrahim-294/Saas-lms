using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class submissionfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentSubmissions_Files_FileId1",
                table: "AssignmentSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentSubmissions_FileId1",
                table: "AssignmentSubmissions");

            migrationBuilder.DropColumn(
                name: "FileId1",
                table: "AssignmentSubmissions");

            migrationBuilder.AlterColumn<string>(
                name: "FileId",
                table: "AssignmentSubmissions",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSubmissions_FileId",
                table: "AssignmentSubmissions",
                column: "FileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentSubmissions_Files_FileId",
                table: "AssignmentSubmissions",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentSubmissions_Files_FileId",
                table: "AssignmentSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentSubmissions_FileId",
                table: "AssignmentSubmissions");

            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "AssignmentSubmissions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileId1",
                table: "AssignmentSubmissions",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSubmissions_FileId1",
                table: "AssignmentSubmissions",
                column: "FileId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentSubmissions_Files_FileId1",
                table: "AssignmentSubmissions",
                column: "FileId1",
                principalTable: "Files",
                principalColumn: "Id");
        }
    }
}
