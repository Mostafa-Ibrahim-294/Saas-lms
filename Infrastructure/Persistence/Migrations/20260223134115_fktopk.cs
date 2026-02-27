using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fktopk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_ModuleItems_ModuleItemId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_ModuleItems_ModuleItemId",
                table: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_ModuleItemId",
                table: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_ModuleItemId",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Lessons",
                newName: "ModuleItemId1");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Assignments",
                newName: "ModuleItemId1");

            migrationBuilder.AlterColumn<int>(
                name: "ModuleItemId",
                table: "Lessons",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ModuleItemId1",
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
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ModuleItemId1",
                table: "Assignments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons",
                column: "ModuleItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments",
                column: "ModuleItemId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_ModuleItems_ModuleItemId1",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_ModuleItems_ModuleItemId1",
                table: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_ModuleItemId1",
                table: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_ModuleItemId1",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "ModuleItemId1",
                table: "Lessons",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ModuleItemId1",
                table: "Assignments",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "ModuleItemId",
                table: "Lessons",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Lessons",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ModuleItemId",
                table: "Assignments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Assignments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_ModuleItemId",
                table: "Lessons",
                column: "ModuleItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ModuleItemId",
                table: "Assignments",
                column: "ModuleItemId");

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
    }
}
