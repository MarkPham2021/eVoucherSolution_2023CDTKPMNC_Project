using eVoucher.Client.Models;
using eVoucher_BUS.FrontendServices;
using eVoucher_ViewModel.Requests.CampaignRequests;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eVoucher.Client.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFrCampaignService _frCampaignService;

        public HomeController(ILogger<HomeController> logger,
                              IFrCampaignService frCampaignService)
        {
            _logger = logger;
            _frCampaignService = frCampaignService;
        }

        public async Task<IActionResult> Index(string keyword = "", int pageIndex = 1, int pageSize = 6)
        {
            var token = HttpContext.Session.GetString("Token");
            var request = new GetManageCampaignPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            string userinfo = User.Identity.Name;
            var campaigns = await _frCampaignService.GetAllCampaignVMsPaging(userinfo, request, token);
            if (campaigns != null)
            {
                return View();
            }
            return NotFound();
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