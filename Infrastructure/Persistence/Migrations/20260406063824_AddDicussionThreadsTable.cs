using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDicussionThreadsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DicussionThreads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    RepliesCount = table.Column<int>(type: "integer", nullable: false),
                    ItemType = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    ModuleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DicussionThreads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DicussionThreads_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DicussionThreads_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DicussionThreads_ModuleItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "ModuleItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DicussionThreads_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DicussionThreads_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DicussionThreadReads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastReadAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DicussionId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DicussionThreadReads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DicussionThreadReads_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DicussionThreadReads_DicussionThreads_DicussionId",
                        column: x => x.DicussionId,
                        principalTable: "DicussionThreads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DicussionThreadReads_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DicussionThreadReplies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Body = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuthorId = table.Column<string>(type: "text", nullable: false),
                    DicussionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DicussionThreadReplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DicussionThreadReplies_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DicussionThreadReplies_DicussionThreads_DicussionId",
                        column: x => x.DicussionId,
                        principalTable: "DicussionThreads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DicussionThreadReads_DicussionId_UserId",
                table: "DicussionThreadReads",
                columns: new[] { "DicussionId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DicussionThreadReads_TenantId",
                table: "DicussionThreadReads",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_DicussionThreadReads_UserId",
                table: "DicussionThreadReads",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DicussionThreadReplies_AuthorId",
                table: "DicussionThreadReplies",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_DicussionThreadReplies_DicussionId",
                table: "DicussionThreadReplies",
                column: "DicussionId");

            migrationBuilder.CreateIndex(
                name: "IX_DicussionThreads_CourseId",
                table: "DicussionThreads",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_DicussionThreads_CreatedBy",
                table: "DicussionThreads",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DicussionThreads_ItemId",
                table: "DicussionThreads",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DicussionThreads_ModuleId",
                table: "DicussionThreads",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_DicussionThreads_TenantId",
                table: "DicussionThreads",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DicussionThreadReads");

            migrationBuilder.DropTable(
                name: "DicussionThreadReplies");

            migrationBuilder.DropTable(
                name: "DicussionThreads");
        }
    }
}
