using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBaseLayer.Migrations
{
    public partial class unicide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdateUser",
                table: "Products",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(250)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductImageName",
                table: "Products",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(250)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductImageLink",
                table: "Products",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(250)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InsertUser",
                table: "Products",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(250)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IPAdress",
                table: "Products",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(250)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FrenchName",
                table: "Products",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(250)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EnglishName",
                table: "Products",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(250)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ArabicName",
                table: "Products",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(250)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdateUser",
                table: "Products",
                type: "VARCHAR(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductImageName",
                table: "Products",
                type: "VARCHAR(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductImageLink",
                table: "Products",
                type: "VARCHAR(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InsertUser",
                table: "Products",
                type: "VARCHAR(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IPAdress",
                table: "Products",
                type: "VARCHAR(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FrenchName",
                table: "Products",
                type: "VARCHAR(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EnglishName",
                table: "Products",
                type: "VARCHAR(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ArabicName",
                table: "Products",
                type: "VARCHAR(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
