using eVoucher.ClientAPI_Integration;
using eVoucher_BUS.FrontendServices;
using eVoucher_ViewModel.Requests.CampaignRequests;
using eVoucher_ViewModel.Requests.CustomerRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVoucher.Partner.Controllers
{
    [Authorize]
    public class CampaignController : BaseController
    {
        private readonly IFrCampaignService _frCampaignService;
        private readonly ICommonService _commonService;
        private readonly CustomerAPIClient _customerAPIClient;

        public CampaignController(IFrCampaignService frCampaignService, 
            ICommonService commonService, CustomerAPIClient customerAPIClient)
        {
            _frCampaignService = frCampaignService;
            _commonService = commonService;
            _customerAPIClient = customerAPIClient;
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
        public async Task<IActionResult> ViewEndedCampaign(string keyword = "", int pageIndex = 1, int pageSize = 3)
        {
            var token = HttpContext.Session.GetString("Token");
            var request = new GetManageCampaignPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            string userinfo = User.Identity.Name;
            var data = await _frCampaignService.GetAllEndedCampaignVMsPaging(userinfo, request, token);
            return View(data);
        }
        [HttpGet("Id")]
        public async Task<IActionResult> CampaignDetail(int Id)
        {
            var token = HttpContext.Session.GetString("Token");
            var campaign = await _frCampaignService.GetCampaignVMById(Id, token);

            if (campaign != null)
            {
                return View(campaign);
            }

            return NotFound("Your campaign not found!");
        }

        [HttpGet]
        public IActionResult GetVoucher(int CampaignGameId)
        {

            var request = new CustomerPlayGameForVoucherRequest()
            {
                AppUserInfo = User.Identity.Name,
                CampaignGameId = CampaignGameId,
                GottenNumber = new Random().Next(1, 1000)
            };

            var token = HttpContext.Session.GetString("Token");
            var response = _customerAPIClient.ClaimVoucher(request, token);
            response.Wait();
            var voucher = response.Result;
            return Json(voucher);
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
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            
            //Prepare for game list check box
            var token = HttpContext.Session.GetString("Token");
            var games = await _frCampaignService.GetAllGames(token);
            ViewBag.games = games;
            //get current data of campaign
            var campaignvm = await _frCampaignService.GetCampaignVMById(id, token);
            //get check status for games
            bool RandomWheelGameChecked =false, TerrisGameChecked =false;
            if (campaignvm.campaignGames.Count >0) 
            {
                if(campaignvm.campaignGames.FirstOrDefault(x => x.Name == "Random Wheel")!= null)
                {
                    RandomWheelGameChecked = true;
                }
                if (campaignvm.campaignGames.FirstOrDefault(x => x.Name == "Terris") != null)
                {
                    TerrisGameChecked = true;
                }
            }
            bool[] GameChecked = { RandomWheelGameChecked, TerrisGameChecked };            
            ViewBag.campaignvm = campaignvm;
            ViewBag.BeginDatestr = _commonService.FormatDatetimeToDatetimeLocalStr(campaignvm.BeginningDate);
            ViewBag.EndDatestr = _commonService.FormatDatetimeToDatetimeLocalStr(campaignvm.EndingDate);
            ViewBag.GameChecked = GameChecked;            
            return View();
        }
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit([FromForm] CampaignEditRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var token = HttpContext.Session.GetString("Token");
            var result = await _frCampaignService.EditCampaign(request, token);
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