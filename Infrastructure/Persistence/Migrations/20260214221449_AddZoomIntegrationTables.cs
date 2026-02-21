using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddZoomIntegrationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZoomIntegrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ZoomUserId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ZoomAccountId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ZoomEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ZoomDisplayName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ZoomAccountType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AccessToken = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    TokenExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastSyncAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoomIntegrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZoomIntegrations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZoomIntegrations_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZoomOAuthStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StateToken = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoomOAuthStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZoomOAuthStates_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZoomOAuthStates_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LiveSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    ZoomMeetingId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ZoomHostId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ZoomJoinUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ZoomStartUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ZoomHostEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ZoomPassword = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    RecordingDuration = table.Column<int>(type: "integer", nullable: true),
                    RecordingUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ScheduledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ActualStartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ActualEndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    HostMemberId = table.Column<int>(type: "integer", nullable: false),
                    ZoomIntegrationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiveSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiveSessions_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LiveSessions_TenantMembers_HostMemberId",
                        column: x => x.HostMemberId,
                        principalTable: "TenantMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LiveSessions_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LiveSessions_ZoomIntegrations_ZoomIntegrationId",
                        column: x => x.ZoomIntegrationId,
                        principalTable: "ZoomIntegrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SessionParticipants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ZoomParticipantId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ZoomParticipantUuid = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ParticipantEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ParticipantName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    DeviceType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Attended = table.Column<bool>(type: "boolean", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: true),
                    JoinCount = table.Column<int>(type: "integer", nullable: false),
                    TotalDuration = table.Column<int>(type: "integer", nullable: true),
                    AttendancePercentage = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    Source = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    IsManuallyMarked = table.Column<bool>(type: "boolean", nullable: false),
                    MarkedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    JoinedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LeftAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MarkedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LiveSessionId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionParticipants_LiveSessions_LiveSessionId",
                        column: x => x.LiveSessionId,
                        principalTable: "LiveSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionParticipants_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LiveSessions_CourseId",
                table: "LiveSessions",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_LiveSessions_HostMemberId",
                table: "LiveSessions",
                column: "HostMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_LiveSessions_TenantId",
                table: "LiveSessions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_LiveSessions_ZoomIntegrationId",
                table: "LiveSessions",
                column: "ZoomIntegrationId");

            migrationBuilder.CreateIndex(
                name: "IX_LiveSessions_ZoomMeetingId",
                table: "LiveSessions",
                column: "ZoomMeetingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionParticipants_Attended",
                table: "SessionParticipants",
                column: "Attended");

            migrationBuilder.CreateIndex(
                name: "IX_SessionParticipants_LiveSessionId",
                table: "SessionParticipants",
                column: "LiveSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionParticipants_StudentId",
                table: "SessionParticipants",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoomIntegrations_TenantId",
                table: "ZoomIntegrations",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoomIntegrations_UserId_TenantId",
                table: "ZoomIntegrations",
                columns: new[] { "UserId", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZoomOAuthStates_StateToken",
                table: "ZoomOAuthStates",
                column: "StateToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZoomOAuthStates_TenantId",
                table: "ZoomOAuthStates",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoomOAuthStates_UserId",
                table: "ZoomOAuthStates",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionParticipants");

            migrationBuilder.DropTable(
                name: "ZoomOAuthStates");

            migrationBuilder.DropTable(
                name: "LiveSessions");

            migrationBuilder.DropTable(
                name: "ZoomIntegrations");
        }
    }
}
