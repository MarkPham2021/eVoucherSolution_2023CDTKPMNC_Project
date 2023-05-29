using eVoucher_BUS.FrontendServices;
using eVoucher_ViewModel.Requests.CampaignRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Drawing.Printing;
using Newtonsoft.Json;
using Castle.MicroKernel.Internal;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<IActionResult> Index(string keyword = "", int pageIndex = 1, int pageSize = 3)
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
        /* 03 method:[httpget] ViewVoucherType + [httpget]CreateVoucherType + [httpost]CreateVoucherType 
         * have been move to VoucherTypeController successfully for the function search for work
         * then the 2 view: ViewVoucherType and CreateVoucherType will no use but remain in folder view Campaign 
         * for later reference
        [HttpGet]
        public async Task<IActionResult> ViewVoucherType(string keyword ="", int pageIndex = 1, int pageSize = 3)
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
            
            return View(data);            
        }
        [HttpGet]
        public async Task<IActionResult> CreateVoucherType(int campaignid)
        {
            var id = HttpContext.Request.RouteValues["id"];
            ViewBag.campaignid = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateVoucherType([FromForm] CampaignCreateVoucherTypeRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            if (!request.IsgetLuckyNumbersRandom)
            {
                var luckynums = request.LuckyNumberstr.Split(" ");
                var luckynumslist = new List<int>();
                foreach(var s in luckynums)
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
            var response = await _frCampaignService.CreateVoucherType(request,token);
            if(response.IsSucceeded)
            {
                return RedirectToAction("ViewVoucherType", new { id = request.CampaignId });
            }
            ViewData["result"] = response.Message;
            return View();
        }
        */
    }
}