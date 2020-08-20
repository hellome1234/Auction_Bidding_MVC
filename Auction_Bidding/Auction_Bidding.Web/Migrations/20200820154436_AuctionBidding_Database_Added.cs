using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Auction_Bidding.Web.Migrations
{
    public partial class AuctionBidding_Database_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auctions",
                columns: table => new
                {
                    AuctionID = table.Column<string>(nullable: false),
                    AuctionName = table.Column<string>(nullable: true),
                    AuctionPrice = table.Column<decimal>(nullable: false),
                    AuctionStartingDate = table.Column<DateTime>(nullable: false),
                    AuctionEndingDate = table.Column<DateTime>(nullable: false),
                    Discription = table.Column<string>(nullable: true),
                    AuctionCoverPhotoURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auctions", x => x.AuctionID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auctions");
        }
    }
}
