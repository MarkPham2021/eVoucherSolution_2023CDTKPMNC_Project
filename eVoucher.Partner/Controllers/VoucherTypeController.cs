using eVoucher_BUS.FrontendServices;
using eVoucher_ViewModel.Requests.CampaignRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eVoucher.Partner.Controllers
{
    [Authorize]
    public class VoucherTypeController : BaseController
    {
        private IFrCampaignService _frCampaignService;
        public VoucherTypeController(IFrCampaignService frCampaignService)
        {
            _frCampaignService = frCampaignService;
        }
        public async Task<IActionResult> Index(string keyword = "", int pageIndex = 1, int pageSize = 3)
        {
            var request = new GetManageCampaignPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var id = HttpContext.Request.RouteValues["id"].ToString();
            var token = HttpContext.Session.GetString("Token");
            ViewData["campaignid"] = id;
            int campaignid;
            bool t = int.TryParse(id, out campaignid);
            var data = await _frCampaignService.GetVoucherTypesOfCampaignPaging(campaignid, request, token);
            var campaign = await _frCampaignService.GetCampaignVMById(campaignid, token);
            ViewData["campaignName"] = campaign.Name;
            return View(data);
        }
        [HttpGet]
        public IActionResult Create()       //int campaignid
        {
            var id = HttpContext.Request.RouteValues["id"];
            ViewBag.campaignid = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CampaignCreateVoucherTypeRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            if (!request.IsgetLuckyNumbersRandom)
            {
                var luckynums = request.LuckyNumberstr.Split(" ");
                var luckynumslist = new List<int>();
                foreach (var s in luckynums)
                {
                    luckynumslist.Add(int.Parse(s));
                }
                request.LuckyNumberstr = JsonConvert.SerializeObject(luckynumslist);
            }
            request.RemainAmount = request.MaxAmount;
            if (string.IsNullOrEmpty(request.LuckyNumberstr))
            {
                request.LuckyNumberstr = "";
            }
            var token = HttpContext.Session.GetString("Token");
            var response = await _frCampaignService.CreateVoucherType(request, token);
            if (response.IsSucceeded)
            {
                return RedirectToAction("Index", new { id = request.CampaignId });
            }
            ViewData["result"] = response.Message;
            return View();
        }
    }
}
