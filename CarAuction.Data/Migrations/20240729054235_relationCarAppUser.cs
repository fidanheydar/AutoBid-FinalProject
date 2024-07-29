using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAuction.Data.Migrations
{
    /// <inheritdoc />
    public partial class relationCarAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Cars",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_AdminId",
                table: "Cars",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_AdminId",
                table: "Cars",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_AdminId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_AdminId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Cars");
        }
    }
}
