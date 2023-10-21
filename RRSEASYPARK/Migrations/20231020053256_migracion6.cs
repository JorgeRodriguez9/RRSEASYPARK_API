using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRSEASYPARK.Migrations
{
    /// <inheritdoc />
    public partial class migracion6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "parkingLots",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "parkingLots");
        }
    }
}
