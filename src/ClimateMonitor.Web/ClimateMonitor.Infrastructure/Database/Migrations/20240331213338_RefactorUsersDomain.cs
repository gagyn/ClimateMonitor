using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimateMonitor.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class RefactorUsersDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AspNetUsers_BaseUserId",
                schema: "climateMonitor",
                table: "Users");

            migrationBuilder.DropTable(
                name: "DeviceUsers",
                schema: "climateMonitor");

            migrationBuilder.DropIndex(
                name: "IX_Users_BaseUserId",
                schema: "climateMonitor",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BaseUserId",
                schema: "climateMonitor",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Devices_Id",
                schema: "climateMonitor",
                table: "AspNetUsers",
                column: "Id",
                principalSchema: "climateMonitor",
                principalTable: "Devices",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Users_Id",
                schema: "climateMonitor",
                table: "AspNetUsers",
                column: "Id",
                principalSchema: "climateMonitor",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Devices_Id",
                schema: "climateMonitor",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Users_Id",
                schema: "climateMonitor",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "BaseUserId",
                schema: "climateMonitor",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DeviceUsers",
                schema: "climateMonitor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaseUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceUsers_AspNetUsers_BaseUserId",
                        column: x => x.BaseUserId,
                        principalSchema: "climateMonitor",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_BaseUserId",
                schema: "climateMonitor",
                table: "Users",
                column: "BaseUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceUsers_BaseUserId",
                schema: "climateMonitor",
                table: "DeviceUsers",
                column: "BaseUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AspNetUsers_BaseUserId",
                schema: "climateMonitor",
                table: "Users",
                column: "BaseUserId",
                principalSchema: "climateMonitor",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
