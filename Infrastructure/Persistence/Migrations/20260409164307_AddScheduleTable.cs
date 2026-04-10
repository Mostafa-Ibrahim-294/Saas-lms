using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddScheduleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    AllDay = table.Column<bool>(type: "boolean", nullable: false),
                    Color = table.Column<string>(type: "character varying(7)", unicode: false, maxLength: 7, nullable: false),
                    RepeatEvent = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    RepeatFrequency = table.Column<int>(type: "integer", nullable: true),
                    RepeatPeriodEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    StartAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.CheckConstraint("CK_Schedule_End_After_Start", "\"EndAt\" > \"StartAt\"");
                    table.CheckConstraint("CK_Schedule_Repeat_Valid", "\"RepeatEvent\" = false OR (\"RepeatFrequency\" IS NOT NULL AND \"RepeatPeriodEnd\" IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_Schedules_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedules_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_CourseId_Type",
                table: "Schedules",
                columns: new[] { "CourseId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TenantId_DateRange",
                table: "Schedules",
                columns: new[] { "TenantId", "StartAt", "EndAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedules");
        }
    }
}
