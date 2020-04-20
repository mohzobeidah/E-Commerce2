using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBaseLayer.Migrations
{
    public partial class tags_tables_new_product_pictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductImageName",
                table: "Products",
                newName: "FrenchDetails");

            migrationBuilder.RenameColumn(
                name: "ProductImageLink",
                table: "Products",
                newName: "FrImportantDetails");

            migrationBuilder.AddColumn<string>(
                name: "ArImportantDetails",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArabicDetails",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnImportantDetails",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnglishDetails",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvaible",
                table: "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsShpping",
                table: "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "productPictures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    size = table.Column<string>(nullable: true),
                    Extenstionsfile = table.Column<string>(nullable: true),
                    DefaultPicture = table.Column<bool>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    InsertUser = table.Column<string>(nullable: true),
                    InsertDate = table.Column<DateTime>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productPictures_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    InsertUser = table.Column<string>(nullable: true),
                    InsertDate = table.Column<DateTime>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tag_Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdTag = table.Column<int>(nullable: false),
                    IdProduct = table.Column<int>(nullable: false),
                    InsertUser = table.Column<string>(nullable: true),
                    InsertDate = table.Column<DateTime>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tag_Product_Products_IdProduct",
                        column: x => x.IdProduct,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tag_Product_tags_IdTag",
                        column: x => x.IdTag,
                        principalTable: "tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productPictures_ProductId",
                table: "productPictures",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tag_Product_IdProduct",
                table: "tag_Product",
                column: "IdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_tag_Product_IdTag",
                table: "tag_Product",
                column: "IdTag");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productPictures");

            migrationBuilder.DropTable(
                name: "tag_Product");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropColumn(
                name: "ArImportantDetails",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ArabicDetails",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "EnImportantDetails",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "EnglishDetails",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsAvaible",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsShpping",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "FrenchDetails",
                table: "Products",
                newName: "ProductImageName");

            migrationBuilder.RenameColumn(
                name: "FrImportantDetails",
                table: "Products",
                newName: "ProductImageLink");
        }
    }
}
