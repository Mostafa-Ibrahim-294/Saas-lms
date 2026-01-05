using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class renameplans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_plansFeature_Features_FeatureId",
                table: "plansFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_plansFeature_Plans_PlanId",
                table: "plansFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_permissions_PermissionId",
                table: "RolePermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_permissions",
                table: "permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_plansFeature",
                table: "plansFeature");

            migrationBuilder.RenameTable(
                name: "permissions",
                newName: "Permissions");

            migrationBuilder.RenameTable(
                name: "plansFeature",
                newName: "PlanFeatures");

            migrationBuilder.RenameIndex(
                name: "IX_plansFeature_PlanId_FeatureId",
                table: "PlanFeatures",
                newName: "IX_PlanFeatures_PlanId_FeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_plansFeature_FeatureId",
                table: "PlanFeatures",
                newName: "IX_PlanFeatures_FeatureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanFeatures",
                table: "PlanFeatures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanFeatures_Features_FeatureId",
                table: "PlanFeatures",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanFeatures_Plans_PlanId",
                table: "PlanFeatures",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanFeatures_Features_FeatureId",
                table: "PlanFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanFeatures_Plans_PlanId",
                table: "PlanFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                table: "RolePermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanFeatures",
                table: "PlanFeatures");

            migrationBuilder.RenameTable(
                name: "Permissions",
                newName: "permissions");

            migrationBuilder.RenameTable(
                name: "PlanFeatures",
                newName: "plansFeature");

            migrationBuilder.RenameIndex(
                name: "IX_PlanFeatures_PlanId_FeatureId",
                table: "plansFeature",
                newName: "IX_plansFeature_PlanId_FeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_PlanFeatures_FeatureId",
                table: "plansFeature",
                newName: "IX_plansFeature_FeatureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_permissions",
                table: "permissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_plansFeature",
                table: "plansFeature",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_plansFeature_Features_FeatureId",
                table: "plansFeature",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_plansFeature_Plans_PlanId",
                table: "plansFeature",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_permissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId",
                principalTable: "permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
