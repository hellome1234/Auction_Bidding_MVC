using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auction_Bidding.Web.Data.Entities
{
    public class Auction
    {
        public string AuctionID { get; set; }
        public string AuctionName { get; set; }
        public decimal AuctionPrice { get; set; }
        public DateTime AuctionStartingDate { get; set; }
        public DateTime AuctionEndingDate { get; set; }
        public string Discription { get; set; }
        public string AuctionCoverPhotoURL { get; set; }

    }
}
