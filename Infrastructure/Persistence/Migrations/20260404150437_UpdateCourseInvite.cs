using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCourseInvite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "CourseInvites",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseInvites_TenantId",
                table: "CourseInvites",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseInvites_Tenants_TenantId",
                table: "CourseInvites",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseInvites_Tenants_TenantId",
                table: "CourseInvites");

            migrationBuilder.DropIndex(
                name: "IX_CourseInvites_TenantId",
                table: "CourseInvites");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "CourseInvites");
        }
    }
}
