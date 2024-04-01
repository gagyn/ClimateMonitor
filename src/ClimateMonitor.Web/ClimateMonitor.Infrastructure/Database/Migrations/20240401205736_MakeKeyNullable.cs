using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimateMonitor.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class MakeKeyNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Devices_Id",
                schema: "climateMonitor",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Users_Id",
                schema: "climateMonitor",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_AspNetUsers_DeviceId",
                schema: "climateMonitor",
                table: "Devices",
                column: "DeviceId",
                principalSchema: "climateMonitor",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AspNetUsers_Id",
                schema: "climateMonitor",
                table: "Users",
                column: "Id",
                principalSchema: "climateMonitor",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_AspNetUsers_DeviceId",
                schema: "climateMonitor",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AspNetUsers_Id",
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
    }
}
