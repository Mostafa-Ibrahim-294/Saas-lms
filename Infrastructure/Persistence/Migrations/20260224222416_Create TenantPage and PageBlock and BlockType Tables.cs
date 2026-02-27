using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateTenantPageandPageBlockandBlockTypeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlockTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: false),
                    Schema = table.Column<Dictionary<string, object>>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenantPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    MetaTitle = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    MetaDescription = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Url = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    IsHomePage = table.Column<bool>(type: "boolean", nullable: false),
                    Views = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TenantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantPages_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Visible = table.Column<bool>(type: "boolean", nullable: false),
                    Props = table.Column<Dictionary<string, object>>(type: "jsonb", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TenantPageId = table.Column<int>(type: "integer", nullable: false),
                    BlockTypeId = table.Column<string>(type: "character varying(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageBlocks_BlockTypes_BlockTypeId",
                        column: x => x.BlockTypeId,
                        principalTable: "BlockTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageBlocks_TenantPages_TenantPageId",
                        column: x => x.TenantPageId,
                        principalTable: "TenantPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PageBlocks_BlockTypeId",
                table: "PageBlocks",
                column: "BlockTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PageBlocks_TenantPageId",
                table: "PageBlocks",
                column: "TenantPageId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPages_TenantId_Url",
                table: "TenantPages",
                columns: new[] { "TenantId", "Url" },
                unique: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageBlocks");

            migrationBuilder.DropTable(
                name: "BlockTypes");

            migrationBuilder.DropTable(
                name: "TenantPages");

        }
    }
}
