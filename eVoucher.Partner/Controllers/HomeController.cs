using eVoucher.Partner.Models;
using eVoucher_BUS.FrontendServices;
using eVoucher_DTO.Models;
using eVoucher_ViewModel.StatisticVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using static eVoucher_ViewModel.StatisticVM.PartnerPeriodicalReport;

namespace eVoucher.Partner.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {        
        private readonly ILogger<HomeController> _logger;
        private readonly IFrPartnerService _frPartnerService;
        private readonly IFrCampaignService _frCampaignService;
        private readonly IFrStatisticService _frStatisticService;
        public HomeController(ILogger<HomeController> logger,
            IFrPartnerService frPartnerService, 
            IFrStatisticService frStatisticService,
            IFrCampaignService frCampaignService)
        {
            _logger = logger;
            _frPartnerService = frPartnerService;
            _frStatisticService = frStatisticService;
            _frCampaignService = frCampaignService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string keyword ="", int periodicaltype = 1, int campaignId = 0)
        {
            string userinfo = User.Identity.Name;
            var request = new PartnerCreatePeriodicalReportRequest()
            {
                Keyword = keyword,
                UserInfo = userinfo,
                PeriodicalType = periodicaltype,
                CampaignId = campaignId,
                NumberOfPeriods = 4,
                LastPeriod = DateTime.Now
            };
            var token = HttpContext.Session.GetString("Token");
            var campaigns = await _frCampaignService.PartnerGetAllActiveCampaignVMs(userinfo,token);
            var _campaign = campaigns.FirstOrDefault(x => x.Id == campaignId);
            var selectlistcampaigns = new List<SelectListItem>();
            foreach (var c in campaigns)
            {
                selectlistcampaigns.Add(new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            }
            if (_campaign != null)
            {
                ViewBag.CampaignName = _campaign.Name;
            }
            else
            {
                ViewBag.CategoryName = "";
            }

            ViewBag.Campaigns = selectlistcampaigns;
            var report = await _frStatisticService.PartnerCreatePeriodicalReport(request, token);
            return View(report);
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