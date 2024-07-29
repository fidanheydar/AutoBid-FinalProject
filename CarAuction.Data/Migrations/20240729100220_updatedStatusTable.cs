using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAuction.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedStatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Statuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_Level",
                table: "Statuses",
                column: "Level",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Statuses_Level",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Statuses");
        }
    }
}
