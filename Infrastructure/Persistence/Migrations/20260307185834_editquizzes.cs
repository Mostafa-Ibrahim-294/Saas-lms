using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editquizzes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_QuestionCategory_QuestionCategoryId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Tenants_TenantId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestion_Question_QuestionId",
                table: "QuizQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestion_Quizzes_QuizId",
                table: "QuizQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizQuestion",
                table: "QuizQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionCategory",
                table: "QuestionCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question",
                table: "Question");

            migrationBuilder.RenameTable(
                name: "QuizQuestion",
                newName: "QuizQuestions");

            migrationBuilder.RenameTable(
                name: "QuestionCategory",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "Question",
                newName: "Questions");

            migrationBuilder.RenameIndex(
                name: "IX_QuizQuestion_QuizId",
                table: "QuizQuestions",
                newName: "IX_QuizQuestions_QuizId");

            migrationBuilder.RenameIndex(
                name: "IX_QuizQuestion_QuestionId",
                table: "QuizQuestions",
                newName: "IX_QuizQuestions_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_TenantId",
                table: "Questions",
                newName: "IX_Questions_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_QuestionCategoryId",
                table: "Questions",
                newName: "IX_Questions_QuestionCategoryId");

            migrationBuilder.AddColumn<int>(
                name: "QuestionId1",
                table: "QuizQuestions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizQuestions",
                table: "QuizQuestions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_QuestionId1",
                table: "QuizQuestions",
                column: "QuestionId1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Categories_QuestionCategoryId",
                table: "Questions",
                column: "QuestionCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Tenants_TenantId",
                table: "Questions",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestions_Questions_QuestionId",
                table: "QuizQuestions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestions_Questions_QuestionId1",
                table: "QuizQuestions",
                column: "QuestionId1",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestions_Quizzes_QuizId",
                table: "QuizQuestions",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "ModuleItemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Categories_QuestionCategoryId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Tenants_TenantId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestions_Questions_QuestionId",
                table: "QuizQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestions_Questions_QuestionId1",
                table: "QuizQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestions_Quizzes_QuizId",
                table: "QuizQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizQuestions",
                table: "QuizQuestions");

            migrationBuilder.DropIndex(
                name: "IX_QuizQuestions_QuestionId1",
                table: "QuizQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "QuestionId1",
                table: "QuizQuestions");

            migrationBuilder.RenameTable(
                name: "QuizQuestions",
                newName: "QuizQuestion");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "Question");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "QuestionCategory");

            migrationBuilder.RenameIndex(
                name: "IX_QuizQuestions_QuizId",
                table: "QuizQuestion",
                newName: "IX_QuizQuestion_QuizId");

            migrationBuilder.RenameIndex(
                name: "IX_QuizQuestions_QuestionId",
                table: "QuizQuestion",
                newName: "IX_QuizQuestion_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_TenantId",
                table: "Question",
                newName: "IX_Question_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_QuestionCategoryId",
                table: "Question",
                newName: "IX_Question_QuestionCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizQuestion",
                table: "QuizQuestion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question",
                table: "Question",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionCategory",
                table: "QuestionCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_QuestionCategory_QuestionCategoryId",
                table: "Question",
                column: "QuestionCategoryId",
                principalTable: "QuestionCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Tenants_TenantId",
                table: "Question",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestion_Question_QuestionId",
                table: "QuizQuestion",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestion_Quizzes_QuizId",
                table: "QuizQuestion",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "ModuleItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
