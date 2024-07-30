using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAuction.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedWinner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WinnerId",
                table: "CarAuctionDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarAuctionDetails_WinnerId",
                table: "CarAuctionDetails",
                column: "WinnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarAuctionDetails_AspNetUsers_WinnerId",
                table: "CarAuctionDetails",
                column: "WinnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarAuctionDetails_AspNetUsers_WinnerId",
                table: "CarAuctionDetails");

            migrationBuilder.DropIndex(
                name: "IX_CarAuctionDetails_WinnerId",
                table: "CarAuctionDetails");

            migrationBuilder.DropColumn(
                name: "WinnerId",
                table: "CarAuctionDetails");
        }
    }
}
