using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStudentSubjectTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Subjects_SubjectId",
                table: "StudentSubjects");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "StudentSubjects",
                newName: "AvailableSubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjects_SubjectId",
                table: "StudentSubjects",
                newName: "IX_StudentSubjects_AvailableSubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjects_StudentId_SubjectId",
                table: "StudentSubjects",
                newName: "IX_StudentSubjects_StudentId_AvailableSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_AvailableSubjects_AvailableSubjectId",
                table: "StudentSubjects",
                column: "AvailableSubjectId",
                principalTable: "AvailableSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_AvailableSubjects_AvailableSubjectId",
                table: "StudentSubjects");

            migrationBuilder.RenameColumn(
                name: "AvailableSubjectId",
                table: "StudentSubjects",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjects_StudentId_AvailableSubjectId",
                table: "StudentSubjects",
                newName: "IX_StudentSubjects_StudentId_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjects_AvailableSubjectId",
                table: "StudentSubjects",
                newName: "IX_StudentSubjects_SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Subjects_SubjectId",
                table: "StudentSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
