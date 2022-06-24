using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Identity.Migrations
{
    public partial class RenameLastnNameColumnNameToLastNameFromAddressTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastnName",
                table: "Address",
                newName: "LastName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Address",
                newName: "LastnName");
        }
    }
}
