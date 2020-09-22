using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Auction_Bidding.Web.Data.Entities;
using Auction_Bidding.Web.Models;
using Auction_Bidding.Web.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Auction_Bidding.Web.Controllers
{
    public class AuctionController : Controller
    {
        //resolving repository dependencies
        private IAuctionRepository _auctions = null;
        private IWebHostEnvironment _env = null;

        public AuctionController(IAuctionRepository auction, IWebHostEnvironment env)
        {
            _auctions = auction;
            _env = env;
        }

        
        //upload auction
       [Route("AddAuction")]
        public ViewResult uploadAuction(bool isSuccess = false,int auctionId = 0)
        {
            ViewBag.isSuccess = isSuccess;
            ViewBag.auctionId = auctionId;
            return View();   
        }

        [Route("AddAuction")]
        [HttpPost]
        public async Task<IActionResult> uploadAuction(AuctionModel model)
        {
                if (ModelState.IsValid)
            {
                //saving image 
                if (model.AuctionCoverPhoto != null)
                {
                    model.AuctionCoverPhotoURL = await SaveImage(model.AuctionCoverPhoto, "Auction/Images");
                    //Saved Success
                    int bookId = await _auctions.SaveAuction(model);
                    if(bookId > 0)
                    {
                        return RedirectToAction(nameof(uploadAuction), new { isSuccess = true, auctionId = bookId });
                    }
                }
                return RedirectToAction(nameof(uploadAuction), new { isSuccess = false });
            }
            return View();
        }

        private async Task<string> SaveImage(IFormFile model,string path)
        {
            //create a name of the photo
            string photoPath =path + Guid.NewGuid().ToString() + "_" + model.FileName;
            string serverPath = Path.Combine(_env.WebRootPath, photoPath);
            await model.CopyToAsync(new FileStream(serverPath, FileMode.Create));
            return "/" + photoPath;
        }

        //edit auction
        [HttpGet]
        [Route("UpdateAuction")]
        public async Task<IActionResult> UpdateAuction(int auctionId = 0 ,bool UpdateStatus = false)
        {
            AuctionModel auctionModel =  await _auctions.GetAuction(auctionId);
            bool isAjaxCall = HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";
            if (isAjaxCall)
            {
                return PartialView(auctionModel);
            }


            return View(auctionModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAuction(AuctionModel model)
        {
            var isAjaxCall = HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";
            if (ModelState.IsValid)
            {
                bool status = await _auctions.UpdateAuction(model);
                if (status)
                {
                    if (isAjaxCall)
                    {
                        return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this,"getAllAuction",await _auctions.GetAuctionsList() )});
                    }
                    return RedirectToAction(nameof(getAllAuction), new { updateStatus = true, auctionId = model.AuctionID });
                }
                if (isAjaxCall)
                {
                    return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "UpdateAuction", await _auctions.GetAuctionsList()) });
                }
                return RedirectToAction(nameof(UpdateAuction), new { updateStatus = false, auctionId = model.AuctionID });

            }
            if (isAjaxCall)
            {
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "UpdateAuction", await _auctions.GetAuctionsList()) });
            }
            return View();  
        }


        //get all auction
        [Route("Auctions")]
        public async Task<IActionResult> getAllAuction()
        {
            var isAjaxCall = HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";
            if (isAjaxCall)
            {
                return PartialView(await _auctions.GetAuctionsList());
            }
            return  View(await _auctions.GetAuctionsList());
        } 

        
        //delete auction
        public async Task<IActionResult> DeleteAuction(int auctionId)
        {
            bool isSuccess = await _auctions.DeleteAuction(auctionId);
            if (isSuccess){
                return Json(new { isSuccess = true, html = Helper.RenderRazorViewToString(this, "getAllAuction", await _auctions.GetAuctionsList() ) });
            }
            return Json(new { isSuccess = false,html = Helper.RenderRazorViewToString(this, "getAllAuction" , await _auctions.GetAuctionsList()) });
        }
    }
}