using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStoreCoreApplicatioc.DAL.Migrations
{
    public partial class AddProductSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Products");
        }
    }
}
