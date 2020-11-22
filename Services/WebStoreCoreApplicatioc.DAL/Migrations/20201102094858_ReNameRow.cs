using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStoreCoreApplicatioc.DAL.Migrations
{
    public partial class ReNameRow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryID",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brand_BrandIdForeigen",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryIDForeigen",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrandIdForeigen",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryIDForeigen",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentCategoryID",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "BrandIdForeigen",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryIDForeigen",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ParentCategoryID",
                table: "Categories");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories",
                column: "ParentId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brand_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brand_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrandId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentId",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "BrandIdForeigen",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryIDForeigen",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentCategoryID",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandIdForeigen",
                table: "Products",
                column: "BrandIdForeigen");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryIDForeigen",
                table: "Products",
                column: "CategoryIDForeigen");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryID",
                table: "Categories",
                column: "ParentCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryID",
                table: "Categories",
                column: "ParentCategoryID",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brand_BrandIdForeigen",
                table: "Products",
                column: "BrandIdForeigen",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryIDForeigen",
                table: "Products",
                column: "CategoryIDForeigen",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
