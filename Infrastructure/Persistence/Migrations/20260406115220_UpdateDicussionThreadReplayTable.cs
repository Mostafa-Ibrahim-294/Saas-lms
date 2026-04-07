using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDicussionThreadReplayTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "DicussionThreadReplies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DicussionThreadReplies_TenantId",
                table: "DicussionThreadReplies",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_DicussionThreadReplies_Tenants_TenantId",
                table: "DicussionThreadReplies",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DicussionThreadReplies_Tenants_TenantId",
                table: "DicussionThreadReplies");

            migrationBuilder.DropIndex(
                name: "IX_DicussionThreadReplies_TenantId",
                table: "DicussionThreadReplies");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "DicussionThreadReplies");
        }
    }
}
