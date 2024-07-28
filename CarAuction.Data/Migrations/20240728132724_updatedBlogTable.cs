using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAuction.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedBlogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Blogs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_AdminId",
                table: "Blogs",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AspNetUsers_AdminId",
                table: "Blogs",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AspNetUsers_AdminId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_AdminId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Blogs");
        }
    }
}
