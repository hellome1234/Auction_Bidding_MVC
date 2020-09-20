using Auction_Bidding.Web.Data;
using Auction_Bidding.Web.Data.Entities;
using Auction_Bidding.Web.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auction_Bidding.Web.Repository
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly AuctionBiddingContext _context = null;

        public AuctionRepository(AuctionBiddingContext context)
        {
            _context = context;
        }

        //Auction Save Method
        public async Task<int> SaveAuction(AuctionModel model)
        {
            Auction AuctionEntity = new Auction()
            {
                AuctionCoverPhotoURL = model.AuctionCoverPhotoURL,
                AuctionEndingDate = model.AuctionEndingDate.GetValueOrDefault(),
                AuctionName = model.AuctionName,
                AuctionPrice = model.AuctionPrice.GetValueOrDefault(),
                AuctionStartingDate = model.AuctionStartingDate.GetValueOrDefault(),
                Discription = model.Discription
            };
            await _context.Auctions.AddAsync(AuctionEntity);
            await _context.SaveChangesAsync();
            return AuctionEntity.AuctionID;
        }

        //Auction Access By AuctionID
        public async Task<AuctionModel> GetAuction(int ID)
        {
            return await _context.Auctions.Where(AuctionEntity => AuctionEntity.AuctionID == ID).Select(AuctionEntity => new AuctionModel()
            {
                AuctionID = AuctionEntity.AuctionID,
                AuctionName = AuctionEntity.AuctionName,
                AuctionCoverPhotoURL = AuctionEntity.AuctionCoverPhotoURL,
                AuctionStartingDate = AuctionEntity.AuctionStartingDate,
                AuctionEndingDate = AuctionEntity.AuctionEndingDate,
                AuctionPrice = AuctionEntity.AuctionPrice,
                Discription = AuctionEntity.Discription
            }).FirstOrDefaultAsync();
        }

        //Auction List 
        public async Task<IEnumerable<AuctionModel>> GetAuctionsList()
        {
            List<Auction> auctionEntites = await _context.Auctions.ToListAsync();
            List<AuctionModel> auctionModels = new List<AuctionModel>();
            if (auctionEntites?.Any() == true)
            {
                foreach (Auction AuctionEntity in auctionEntites)
                {
                    auctionModels.Add(new AuctionModel()
                    {
                        AuctionID = AuctionEntity.AuctionID,
                        AuctionName = AuctionEntity.AuctionName,
                        AuctionCoverPhotoURL = AuctionEntity.AuctionCoverPhotoURL,
                        AuctionStartingDate = AuctionEntity.AuctionStartingDate,
                        AuctionEndingDate = AuctionEntity.AuctionEndingDate,
                        AuctionPrice = AuctionEntity.AuctionPrice,
                        Discription = AuctionEntity.Discription
                    });
                }
            }

            return auctionModels;
        }

        //Auction Upadate 

        public async Task<bool> UpdateAuction(AuctionModel model)
        {
            Auction AuctionEntity = await _context.Auctions.Where(AuctionEntity => AuctionEntity.AuctionID == model.AuctionID).FirstOrDefaultAsync();
            if (AuctionEntity != null)
            {
                AuctionEntity.AuctionName = model.AuctionName;
                AuctionEntity.AuctionPrice = model.AuctionPrice.GetValueOrDefault();
                AuctionEntity.AuctionStartingDate = model.AuctionStartingDate.GetValueOrDefault();
                AuctionEntity.AuctionEndingDate = model.AuctionEndingDate.GetValueOrDefault();
                AuctionEntity.Discription = model.Discription;
                int i = await _context.SaveChangesAsync();
                if (i == 1)
                {
                    //if save is successfull 
                    return true;
                }
            }
            //if save is unsuccessful
            return false;
        }

        //Auction Delete 
        public async Task<bool> DeleteAuction(int auctionId)
        {
            Auction AuctionEntity = await _context.Auctions.Where(AuctionEntity => AuctionEntity.AuctionID == auctionId).FirstOrDefaultAsync();
            if (AuctionEntity != null)
            {
                _context.Auctions.Remove(AuctionEntity);
                int i = await _context.SaveChangesAsync();
                if (i == 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
