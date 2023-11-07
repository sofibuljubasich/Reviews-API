using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewAPP.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_Categories_CategoriesId",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Places_PlaceID",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "Hours");

            migrationBuilder.DropIndex(
                name: "IX_Places_CategoriesId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "CategoriesId",
                table: "Places");

            migrationBuilder.RenameColumn(
                name: "PlaceID",
                table: "Reviews",
                newName: "PlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_PlaceID",
                table: "Reviews",
                newName: "IX_Reviews_PlaceId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Places",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "PlaceCategory",
                columns: table => new
                {
                    PlaceID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceCategory", x => new { x.PlaceID, x.CategoryID });
                    table.ForeignKey(
                        name: "FK_PlaceCategory_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaceCategory_Places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaceCategory_CategoryID",
                table: "PlaceCategory",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Places_PlaceId",
                table: "Reviews",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Places_PlaceId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "PlaceCategory");

            migrationBuilder.RenameColumn(
                name: "PlaceId",
                table: "Reviews",
                newName: "PlaceID");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_PlaceId",
                table: "Reviews",
                newName: "IX_Reviews_PlaceID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Places",
                newName: "ID");

            migrationBuilder.AddColumn<int>(
                name: "CategoriesId",
                table: "Places",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Hours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceID = table.Column<int>(type: "int", nullable: false),
                    Close = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Open = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hours_Places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "Places",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Places_CategoriesId",
                table: "Places",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Hours_PlaceID",
                table: "Hours",
                column: "PlaceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Categories_CategoriesId",
                table: "Places",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Places_PlaceID",
                table: "Reviews",
                column: "PlaceID",
                principalTable: "Places",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
