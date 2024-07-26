using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAuction.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedCarImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Bans_BanId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_Colors_ColorId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_Fuels_FuelId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_Models_ModelId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_CarAuctionDetail_Car_Id",
                table: "CarAuctionDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarAuctionDetail",
                table: "CarAuctionDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Car",
                table: "Car");

            migrationBuilder.RenameTable(
                name: "CarAuctionDetail",
                newName: "CarAuctionDetails");

            migrationBuilder.RenameTable(
                name: "Car",
                newName: "Cars");

            migrationBuilder.RenameIndex(
                name: "IX_Car_ModelId",
                table: "Cars",
                newName: "IX_Cars_ModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Car_FuelId",
                table: "Cars",
                newName: "IX_Cars_FuelId");

            migrationBuilder.RenameIndex(
                name: "IX_Car_ColorId",
                table: "Cars",
                newName: "IX_Cars_ColorId");

            migrationBuilder.RenameIndex(
                name: "IX_Car_BanId",
                table: "Cars",
                newName: "IX_Cars_BanId");

            migrationBuilder.AddColumn<int>(
                name: "Power",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarAuctionDetails",
                table: "CarAuctionDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CarImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isMain = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarImages_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarImages_CarId",
                table: "CarImages",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarAuctionDetails_Cars_Id",
                table: "CarAuctionDetails",
                column: "Id",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Bans_BanId",
                table: "Cars",
                column: "BanId",
                principalTable: "Bans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Colors_ColorId",
                table: "Cars",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Fuels_FuelId",
                table: "Cars",
                column: "FuelId",
                principalTable: "Fuels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Models_ModelId",
                table: "Cars",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarAuctionDetails_Cars_Id",
                table: "CarAuctionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Bans_BanId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Colors_ColorId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Fuels_FuelId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Models_ModelId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "CarImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarAuctionDetails",
                table: "CarAuctionDetails");

            migrationBuilder.DropColumn(
                name: "Power",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "Cars",
                newName: "Car");

            migrationBuilder.RenameTable(
                name: "CarAuctionDetails",
                newName: "CarAuctionDetail");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_ModelId",
                table: "Car",
                newName: "IX_Car_ModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_FuelId",
                table: "Car",
                newName: "IX_Car_FuelId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_ColorId",
                table: "Car",
                newName: "IX_Car_ColorId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_BanId",
                table: "Car",
                newName: "IX_Car_BanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car",
                table: "Car",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarAuctionDetail",
                table: "CarAuctionDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Bans_BanId",
                table: "Car",
                column: "BanId",
                principalTable: "Bans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Colors_ColorId",
                table: "Car",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Fuels_FuelId",
                table: "Car",
                column: "FuelId",
                principalTable: "Fuels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Models_ModelId",
                table: "Car",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarAuctionDetail_Car_Id",
                table: "CarAuctionDetail",
                column: "Id",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
