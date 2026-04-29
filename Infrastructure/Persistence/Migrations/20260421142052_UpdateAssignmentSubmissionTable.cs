using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAssignmentSubmissionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_AssignmentSubmissions_AssignmentSubmissionId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_AssignmentSubmissionId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "AssignmentSubmissionId",
                table: "Files");

            migrationBuilder.AddColumn<string>(
                name: "FileId",
                table: "AssignmentSubmissions",
                type: "text",
                nullable: true);

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

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "AssignmentSubmissions");

            migrationBuilder.AddColumn<int>(
                name: "AssignmentSubmissionId",
                table: "Files",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_AssignmentSubmissionId",
                table: "Files",
                column: "AssignmentSubmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_AssignmentSubmissions_AssignmentSubmissionId",
                table: "Files",
                column: "AssignmentSubmissionId",
                principalTable: "AssignmentSubmissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
