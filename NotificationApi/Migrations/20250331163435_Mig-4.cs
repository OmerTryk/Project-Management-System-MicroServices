using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotificationApi.Migrations
{
    /// <inheritdoc />
    public partial class Mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectNotificationMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectNotificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectNotificationMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectNotificationMembers_ProjectNotifications_ProjectNotificationId",
                        column: x => x.ProjectNotificationId,
                        principalTable: "ProjectNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectNotificationMembers_ProjectNotificationId",
                table: "ProjectNotificationMembers",
                column: "ProjectNotificationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectNotificationMembers");
        }
    }
}
