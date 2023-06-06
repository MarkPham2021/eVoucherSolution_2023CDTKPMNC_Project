using eVoucher.Client.Models;
using eVoucher.ClientAPI_Integration;
using eVoucher_BUS.FrontendServices;
using eVoucher_DTO.Models;
using eVoucher_Utility.Constants;
using eVoucher_ViewModel.Requests.CampaignRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace eVoucher.Client.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFrCampaignService _frCampaignService;
        private readonly IFrPartnerService _frPartnerService;
        private readonly IFrCustomerService _frCustomerService;
        private readonly CampaignAPIClient _campaignAPIClient;
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger,
                              IConfiguration configuration,
                              IFrCampaignService frCampaignService,
                              IFrPartnerService frPartnerService,
                              IFrCustomerService frCustomerService,
                              CampaignAPIClient campaignAPIClient)
        {
            _logger = logger;
            _frCampaignService = frCampaignService;
            _frPartnerService = frPartnerService;
            _frCustomerService = frCustomerService;
            _campaignAPIClient = campaignAPIClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(string keyword = "",int filter=0,int categoryId=0, 
            int pageIndex = 1, int pageSize = 8, 
            string currentAddress= "Số 86 Đ. Lê Thánh Tôn, Bến Nghé, Quận 1, Thành phố Hồ Chí Minh 710212, Vietnam")
        {
            var token = HttpContext.Session.GetString("Token");
            var request = new GetCustomerCampaignPagingRequest()
            {
                keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                filter = filter,
                categoryId = categoryId,
                currentAddress = currentAddress
            };
            string userinfo = User.Identity.Name;
            var categories = await _frPartnerService.GetPartnerCategoriesAsync();
            var selectlistpartnercategory = new List<SelectListItem>();
            foreach (PartnerCategory category in categories)
            {
                selectlistpartnercategory.Add(new SelectListItem { Text = category.Name, Value = category.Id.ToString() });
            }
            ViewBag.Categories = selectlistpartnercategory;
            var currentUser = await _frCustomerService.GetCustomerFullInfoByUserInfo(userinfo, token);
            ViewBag.currentAddress = currentUser.Address;
            /*
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
             */
            var page = await _frCustomerService.GetCustomerCampaignVMsPaging(request, token);
            return View(page);
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