using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newfunctions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_post_UserId",
                table: "post",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_post_Users_UserId",
                table: "post",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_post_Users_UserId",
                table: "post");

            migrationBuilder.DropIndex(
                name: "IX_post_UserId",
                table: "post");
        }
    }
}
