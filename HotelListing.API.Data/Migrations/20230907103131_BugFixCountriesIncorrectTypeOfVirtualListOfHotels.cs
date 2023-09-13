using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListing.API.Migrations
{
    /// <inheritdoc />
    public partial class BugFixCountriesIncorrectTypeOfVirtualListOfHotels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Countries_CountryId",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_CountryId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Countries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Countries",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CountryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CountryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3,
                column: "CountryId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CountryId",
                table: "Countries",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Countries_CountryId",
                table: "Countries",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }
    }
}
