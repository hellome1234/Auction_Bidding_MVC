using Auction_Bidding.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auction_Bidding.Web.Data
{
    public class AuctionBiddingContext:DbContext
    {
        public AuctionBiddingContext(DbContextOptions<AuctionBiddingContext> options):base(options)
        {

        }

        public DbSet<Auction> Auctions { get; set; }


    }
}
