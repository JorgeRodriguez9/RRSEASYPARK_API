using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRSEASYPARK.Migrations
{
    /// <inheritdoc />
    public partial class migracion8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Telephone",
                table: "propietaryParks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "propietaryParks");
        }
    }
}
