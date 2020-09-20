using Auction_Bidding.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction_Bidding.Web.Repository
{
    public interface IAuctionRepository
    {
        Task<bool> DeleteAuction(int auctionId);
        Task<AuctionModel> GetAuction(int ID);
        Task<IEnumerable<AuctionModel>> GetAuctionsList();
        Task<int> SaveAuction(AuctionModel model);
        Task<bool> UpdateAuction(AuctionModel model);
    }
}