using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlueRaven.Data.Migrations
{
    public partial class AddFaviconUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FaviconUrl",
                table: "Blogs",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LastModified", "Slug" },
                values: new object[] { new DateTime(2018, 9, 5, 1, 1, 3, 919, DateTimeKind.Utc), "2018/7/6/first-post" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "LastModified", "Slug" },
                values: new object[] { new DateTime(2018, 9, 5, 1, 1, 3, 920, DateTimeKind.Utc), "2018/7/13/next-post" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaviconUrl",
                table: "Blogs");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LastModified", "Slug" },
                values: new object[] { new DateTime(2018, 8, 9, 2, 50, 24, 752, DateTimeKind.Utc), "" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "LastModified", "Slug" },
                values: new object[] { new DateTime(2018, 8, 9, 2, 50, 24, 752, DateTimeKind.Utc), "" });
        }
    }
}
