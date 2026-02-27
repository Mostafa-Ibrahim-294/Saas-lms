using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class edititemkeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleItemConditions_ModuleItems_RequiredModuleItemId",
                table: "ModuleItemConditions");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleItemConditions_ModuleItems_RequiredModuleItemId",
                table: "ModuleItemConditions",
                column: "RequiredModuleItemId",
                principalTable: "ModuleItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleItemConditions_ModuleItems_RequiredModuleItemId",
                table: "ModuleItemConditions");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleItemConditions_ModuleItems_RequiredModuleItemId",
                table: "ModuleItemConditions",
                column: "RequiredModuleItemId",
                principalTable: "ModuleItems",
                principalColumn: "Id");
        }
    }
}
