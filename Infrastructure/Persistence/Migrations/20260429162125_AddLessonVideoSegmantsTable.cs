using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLessonVideoSegmantsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LessonVideoSegmants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartSecond = table.Column<int>(type: "integer", nullable: false),
                    EndSecond = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LessonViewId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonVideoSegmants", x => x.Id);
                    table.CheckConstraint("CK_LessonVideoSegmants_Seconds", "\"StartSecond\" >= 0 AND \"EndSecond\" > \"StartSecond\"");
                    table.ForeignKey(
                        name: "FK_LessonVideoSegmants_LessonViews_LessonViewId",
                        column: x => x.LessonViewId,
                        principalTable: "LessonViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonVideoSegmants_LessonViewId",
                table: "LessonVideoSegmants",
                column: "LessonViewId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonVideoSegmants");
        }
    }
}
