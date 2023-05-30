using eVoucher.Client.Models;
using eVoucher.ClientAPI_Integration;
using eVoucher_BUS.FrontendServices;
using eVoucher_DTO.Models;
using eVoucher_Utility.Constants;
using eVoucher_ViewModel.Requests.CampaignRequests;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace eVoucher.Client.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFrCampaignService _frCampaignService;
        private readonly CampaignAPIClient _campaignAPIClient;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger,
                              IConfiguration configuration,
                              IFrCampaignService frCampaignService,
                              CampaignAPIClient campaignAPIClient)
        {
            _logger = logger;
            _frCampaignService = frCampaignService;
            _campaignAPIClient = campaignAPIClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(string keyword = "", int pageIndex = 1, int pageSize = 6)
        {
            var token = HttpContext.Session.GetString("Token");
            //var request = new GetManageCampaignPagingRequest()
            //{
            //    Keyword = keyword,
            //    PageIndex = pageIndex,
            //    PageSize = pageSize
            //};
            //string userinfo = User.Identity.Name;
            //var campaigns = await _frCampaignService.GetAllCampaignVMsPaging(userinfo, request, token);

            var campaigns = await _campaignAPIClient.GetAllCampaignVMsAsync(token);

            if (campaigns != null)
            {
                string BaseAdress = _configuration[SystemConstants.AppSettings.BaseAddress];

                foreach (var item in campaigns)
                {
                    item.ImagePath = BaseAdress + item.ImagePath;
                }
                return View(campaigns);
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