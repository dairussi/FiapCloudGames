using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiapCloudGames.Migrations
{
    /// <inheritdoc />
    public partial class gamepur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "User",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "GamePromotion",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "int", nullable: false),
                    PromotionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePromotion", x => new { x.GamesId, x.PromotionsId });
                    table.ForeignKey(
                        name: "FK_GamePromotion_Game_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePromotion_Promotion_PromotionsId",
                        column: x => x.PromotionsId,
                        principalTable: "Promotion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePurchase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    DataGamePurchase = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PromotionValue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PromotionId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePurchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamePurchase_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GamePurchase_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPromotion",
                columns: table => new
                {
                    PromotionsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPromotion", x => new { x.PromotionsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserPromotion_Promotion_PromotionsId",
                        column: x => x.PromotionsId,
                        principalTable: "Promotion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPromotion_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePromotion_PromotionsId",
                table: "GamePromotion",
                column: "PromotionsId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePurchase_GameId",
                table: "GamePurchase",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePurchase_UserId",
                table: "GamePurchase",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPromotion_UsersId",
                table: "UserPromotion",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePromotion");

            migrationBuilder.DropTable(
                name: "GamePurchase");

            migrationBuilder.DropTable(
                name: "UserPromotion");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "User");
        }
    }
}
