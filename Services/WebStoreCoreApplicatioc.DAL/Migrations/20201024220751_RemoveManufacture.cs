using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStoreCoreApplicatioc.DAL.Migrations
{
    public partial class RemoveManufacture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
