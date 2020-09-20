using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Auction_Bidding.Web.Models.ValidationAttributes
{
    public class AuctionDateValidationAttribute:ValidationAttribute
    {

        //starting date property to check if the ending date is earlier then starting date
        public DateTime StartingDate { get; set; }
        public AuctionDateValidationAttribute(DateTime startingDate, string ErrorMessage):base(ErrorMessage) {
            StartingDate = startingDate;
        }

        public AuctionDateValidationAttribute(DateTime startingDate){
            StartingDate = startingDate;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                DateTime EndingDate = (DateTime)value;
                if(EndingDate > StartingDate)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessage ?? "Please provide the closing date further from opening date");
                } 

            }
            return new ValidationResult(ErrorMessage ?? "Please provide closing date for auction");
        }

    }
}
