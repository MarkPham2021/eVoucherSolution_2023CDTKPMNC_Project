using eVoucher.ClientAPI_Integration;
using eVoucher_BUS.FrontendServices;
using eVoucher_ViewModel.Requests.CampaignRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVoucher.Partner.Controllers
{
    [Authorize]
    public class CampaignController : BaseController
    {
        private IFrCampaignService _frCampaignService;
        

        public CampaignController(IFrCampaignService frCampaignService)
        {
            _frCampaignService = frCampaignService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string keyword="", int pageIndex = 1, int pageSize = 3)
        {
            var token = HttpContext.Session.GetString("Token");
            var request = new GetManageCampaignPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            string userinfo = User.Identity.Name;            
            var data = await _frCampaignService.GetAllCampaignVMsPaging(userinfo, request, token);
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //Prepare for PartnerId
            string userinfo = User.Identity.Name;
            var infos = userinfo.Split('|');
            ViewBag.partnerid = int.Parse(infos[0]);
            //Prepare for game list check box
            var token = HttpContext.Session.GetString("Token");
            var games = await _frCampaignService.GetAllGames(token);
            ViewBag.games = games;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CampaignCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var token = HttpContext.Session.GetString("Token");
            var result = await _frCampaignService.CreateCampaign(request, token);
            if (!result.IsSucceeded)
            {
                ViewData["result"] = "unsuccess";
            }
            else
                ViewData["result"] = "success";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> CreateVoucherType(int campaignid)
        {
            return View();
        }
    }
}