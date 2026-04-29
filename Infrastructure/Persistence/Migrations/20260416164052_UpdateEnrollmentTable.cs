using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEnrollmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_StudentId_CourseId",
                table: "Enrollments");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "ModuleItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentModuleId",
                table: "Enrollments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentModuleItemId",
                table: "Enrollments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InvitedBy",
                table: "Enrollments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Enrollments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CurrentModuleId",
                table: "Enrollments",
                column: "CurrentModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CurrentModuleItemId",
                table: "Enrollments",
                column: "CurrentModuleItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_InvitedBy",
                table: "Enrollments",
                column: "InvitedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_OrderId",
                table: "Enrollments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId_CourseId_TenantId",
                table: "Enrollments",
                columns: new[] { "StudentId", "CourseId", "TenantId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_ModuleItems_CurrentModuleItemId",
                table: "Enrollments",
                column: "CurrentModuleItemId",
                principalTable: "ModuleItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Modules_CurrentModuleId",
                table: "Enrollments",
                column: "CurrentModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Orders_OrderId",
                table: "Enrollments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_TenantMembers_InvitedBy",
                table: "Enrollments",
                column: "InvitedBy",
                principalTable: "TenantMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_ModuleItems_CurrentModuleItemId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Modules_CurrentModuleId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Orders_OrderId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_TenantMembers_InvitedBy",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_CurrentModuleId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_CurrentModuleItemId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_InvitedBy",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_OrderId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_StudentId_CourseId_TenantId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "ModuleItems");

            migrationBuilder.DropColumn(
                name: "CurrentModuleId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "CurrentModuleItemId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "InvitedBy",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Enrollments");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId_CourseId",
                table: "Enrollments",
                columns: new[] { "StudentId", "CourseId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
