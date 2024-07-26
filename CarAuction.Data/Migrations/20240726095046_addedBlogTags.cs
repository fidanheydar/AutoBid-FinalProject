using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAuction.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedBlogTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BansTags_Blogs_BlogId",
                table: "BansTags");

            migrationBuilder.DropForeignKey(
                name: "FK_BansTags_Tags_TagId",
                table: "BansTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BansTags",
                table: "BansTags");

            migrationBuilder.RenameTable(
                name: "BansTags",
                newName: "BlogTags");

            migrationBuilder.RenameColumn(
                name: "SectionImage",
                table: "Blogs",
                newName: "SectionImageUrl");

            migrationBuilder.RenameColumn(
                name: "BaseImage",
                table: "Blogs",
                newName: "BaseImageUrl");

            migrationBuilder.RenameIndex(
                name: "IX_BansTags_BlogId",
                table: "BlogTags",
                newName: "IX_BlogTags_BlogId");

            migrationBuilder.AddColumn<Guid>(
                name: "BlogId",
                table: "Blogs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "BlogTags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "BlogTags",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BlogTags",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "BlogTags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogTags",
                table: "BlogTags",
                columns: new[] { "TagId", "BlogId" });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogId",
                table: "Blogs",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Blogs_BlogId",
                table: "Blogs",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTags_Blogs_BlogId",
                table: "BlogTags",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTags_Tags_TagId",
                table: "BlogTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Blogs_BlogId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTags_Blogs_BlogId",
                table: "BlogTags");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTags_Tags_TagId",
                table: "BlogTags");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_BlogId",
                table: "Blogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogTags",
                table: "BlogTags");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BlogTags");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BlogTags");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BlogTags");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BlogTags");

            migrationBuilder.RenameTable(
                name: "BlogTags",
                newName: "BansTags");

            migrationBuilder.RenameColumn(
                name: "SectionImageUrl",
                table: "Blogs",
                newName: "SectionImage");

            migrationBuilder.RenameColumn(
                name: "BaseImageUrl",
                table: "Blogs",
                newName: "BaseImage");

            migrationBuilder.RenameIndex(
                name: "IX_BlogTags_BlogId",
                table: "BansTags",
                newName: "IX_BansTags_BlogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BansTags",
                table: "BansTags",
                columns: new[] { "TagId", "BlogId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BansTags_Blogs_BlogId",
                table: "BansTags",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BansTags_Tags_TagId",
                table: "BansTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
