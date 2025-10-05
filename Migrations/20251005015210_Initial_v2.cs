using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiapCloudGames.Migrations
{
    /// <inheritdoc />
    public partial class Initial_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName_Name",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Email_Email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Price_Value",
                table: "Games",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "AgeRating_Rating",
                table: "Games",
                newName: "AgeRating");

            migrationBuilder.RenameColumn(
                name: "AgeRating_MinimiumAge",
                table: "Games",
                newName: "MinimiumAge");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "FullName_Name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "Email_Email");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Games",
                newName: "Price_Value");

            migrationBuilder.RenameColumn(
                name: "MinimiumAge",
                table: "Games",
                newName: "AgeRating_MinimiumAge");

            migrationBuilder.RenameColumn(
                name: "AgeRating",
                table: "Games",
                newName: "AgeRating_Rating");
        }
    }
}
