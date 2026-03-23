using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class testttttttt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonView_Lessons_ModuleItemId",
                table: "LessonView");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonView_Students_StudentId",
                table: "LessonView");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonView",
                table: "LessonView");

            migrationBuilder.RenameTable(
                name: "LessonView",
                newName: "LessonViews");

            migrationBuilder.RenameIndex(
                name: "IX_LessonView_StudentId",
                table: "LessonViews",
                newName: "IX_LessonViews_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonView_ModuleItemId",
                table: "LessonViews",
                newName: "IX_LessonViews_ModuleItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonViews",
                table: "LessonViews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonViews_Lessons_ModuleItemId",
                table: "LessonViews",
                column: "ModuleItemId",
                principalTable: "Lessons",
                principalColumn: "ModuleItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonViews_Students_StudentId",
                table: "LessonViews",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonViews_Lessons_ModuleItemId",
                table: "LessonViews");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonViews_Students_StudentId",
                table: "LessonViews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonViews",
                table: "LessonViews");

            migrationBuilder.RenameTable(
                name: "LessonViews",
                newName: "LessonView");

            migrationBuilder.RenameIndex(
                name: "IX_LessonViews_StudentId",
                table: "LessonView",
                newName: "IX_LessonView_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonViews_ModuleItemId",
                table: "LessonView",
                newName: "IX_LessonView_ModuleItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonView",
                table: "LessonView",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonView_Lessons_ModuleItemId",
                table: "LessonView",
                column: "ModuleItemId",
                principalTable: "Lessons",
                principalColumn: "ModuleItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonView_Students_StudentId",
                table: "LessonView",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
