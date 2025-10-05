using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiapCloudGames.Migrations
{
    /// <inheritdoc />
    public partial class modified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PuplicId",
                table: "Users",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "NickName",
                table: "Users",
                newName: "Nick");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "Users",
                newName: "PuplicId");

            migrationBuilder.RenameColumn(
                name: "Nick",
                table: "Users",
                newName: "NickName");
        }
    }
}
