using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Auction_Bidding.Web.Models.ValidationAttributes;

namespace Auction_Bidding.Web.Models
{
    public class AuctionModel
    {
        public int AuctionID { get; set; }
       
        [Display(Name = "Auction Name")]
        [Required(ErrorMessage = "Auction name is must")]
        public string AuctionName { get; set; }

        [Display(Name = "Auction Price")]
        [Required(ErrorMessage = "Auction Price is must")]
        public decimal? AuctionPrice { get; set; }

        [Display(Name = "Starting Date")]
        [Required(ErrorMessage = "Staring Date to Auction is must")]
        public DateTime? AuctionStartingDate { get ;  set; }

        [Display(Name = "Closing Date")]
        public DateTime? AuctionEndingDate { get; set; }

        public string Discription { get; set; }

        public string AuctionCoverPhotoURL { get; set; }
        public IFormFile AuctionCoverPhoto { get; set; }
    }
}
