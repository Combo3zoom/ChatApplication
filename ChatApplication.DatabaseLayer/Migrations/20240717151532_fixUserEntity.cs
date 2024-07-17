using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApplication.Database.Migrations
{
    /// <inheritdoc />
    public partial class fixUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_ConnectionId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ConnectionId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConnectionId",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ConnectionId",
                table: "Users",
                column: "ConnectionId",
                unique: true);
        }
    }
}
