using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripLogServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mg5commentsfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_AppUsers_AppUserId",
                table: "Trips");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Trips",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_AppUsers_AppUserId",
                table: "Trips",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_AppUsers_AppUserId",
                table: "Trips");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Trips",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_AppUsers_AppUserId",
                table: "Trips",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }
    }
}
