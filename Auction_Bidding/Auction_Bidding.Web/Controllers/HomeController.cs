using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Auction_Bidding.Web.Models;
using Auction_Bidding.Web.Repository;

namespace Auction_Bidding.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuctionRepository _auction;

        public HomeController(ILogger<HomeController> logger,IAuctionRepository auction)
        {
            _logger = logger;
            _auction = auction;
        }

        public async Task<IActionResult> Index()
        {
            
            return View(await _auction.GetAuctionsList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
