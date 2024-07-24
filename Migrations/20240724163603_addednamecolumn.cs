using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nayan_Assignement3.Migrations
{
    /// <inheritdoc />
    public partial class addednamecolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "StoreProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "StoreProducts");
        }
    }
}
