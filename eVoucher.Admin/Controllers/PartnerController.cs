using eVoucher.Admin.Models;
using eVoucher_DTO.Models;
using eVoucher_ViewModel.Requests.CampaignRequests;
using eVoucher_ViewModel.StatisticVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Drawing.Printing;
using eVoucher_BUS.FrontendServices;

namespace eVoucher.Admin.Controllers
{
    [Authorize]
    public class PartnerController : Controller
    {
        
        private readonly IFrPartnerService _frPartnerService;
        private readonly IFrStatisticService _frStatisticService;
        public PartnerController(IFrPartnerService frPartnerService,
            IFrStatisticService frStatisticService)
        {
            _frPartnerService = frPartnerService;
            _frStatisticService = frStatisticService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string keyword ="", int categoryId = 0, int pageIndex = 1, int pageSize = 8)
        {
            var token = HttpContext.Session.GetString("Token");
            var request = new GetCustomerCampaignPagingRequest()
            {
                keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                categoryId = categoryId
            };
            string userinfo = User.Identity.Name;
            var categories = await _frPartnerService.GetPartnerCategoriesAsync();
            var _category = categories.FirstOrDefault(x => x.Id == categoryId);
            var selectlistpartnercategory = new List<SelectListItem>();
            foreach (PartnerCategory category in categories)
            {
                selectlistpartnercategory.Add(new SelectListItem { Text = category.Name, Value = category.Id.ToString() });
            }
            if (_category != null)
            {
                ViewBag.CategoryName = _category.Name;
            }
            else
            {
                ViewBag.CategoryName = "";
            }

            ViewBag.Categories = selectlistpartnercategory;
            var report = await _frStatisticService.CreatePeriodicalReport(request, token);
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
