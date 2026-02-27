using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editfks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_ModuleItems_ModuleItemId1",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_ModuleItems_ModuleItemId1",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_ModuleItemId1",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_ModuleItemId1",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "ModuleItemId1",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "ModuleItemId1",
                table: "Assignments");

            migrationBuilder.AlterColumn<int>(
                name: "ModuleItemId",
                table: "Lessons",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ModuleItemId",
                table: "Assignments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_ModuleItems_ModuleItemId",
                table: "Assignments",
                column: "ModuleItemId",
                principalTable: "ModuleItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_ModuleItems_ModuleItemId",
                table: "Lessons",
                column: "ModuleItemId",
                principalTable: "ModuleItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_ModuleItems_ModuleItemId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_ModuleItems_ModuleItemId",
                table: "Lessons");

            migrationBuilder.AlterColumn<int>(
                name: "ModuleItemId",
                table: "Lessons",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ModuleItemId1",
                table: "Lessons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ModuleItemId",
                table: "Assignments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ModuleItemId1",
                table: "Assignments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_ModuleItemId1",
                table: "Lessons",
                column: "ModuleItemId1");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ModuleItemId1",
                table: "Assignments",
                column: "ModuleItemId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_ModuleItems_ModuleItemId1",
                table: "Assignments",
                column: "ModuleItemId1",
                principalTable: "ModuleItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_ModuleItems_ModuleItemId1",
                table: "Lessons",
                column: "ModuleItemId1",
                principalTable: "ModuleItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
