using eVoucher_BUS.FrontendServices;
using eVoucher_DTO.Models;
using eVoucher_ViewModel.Requests.CampaignRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eVoucher.Admin.Controllers
{
    [Authorize]
    public class CampaignController : Controller
    {
        private readonly IFrCampaignService _frCampaignService;
        private readonly IFrPartnerService _frPartnerService;

        public CampaignController(IFrCampaignService frCampaignService, IFrPartnerService frPartnerService)
        {
            _frCampaignService = frCampaignService;
            _frPartnerService = frPartnerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword = "", int categoryId = 0, int pageIndex = 1, int pageSize = 4)
        {
            var token = HttpContext.Session.GetString("Token");
            var request = new GetAdminCampaignsPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                CategoryId = categoryId
            };
            string userinfo = User.Identity.Name;
            var categories = await _frPartnerService.GetPartnerCategoriesAsync(token);
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
            var pageresult = await _frCampaignService.GetAdminCampaignVMsPaging(request, token);
            return View(pageresult);
        }

        [HttpGet("drop/{id}")]
        public async Task<IActionResult> Drop(int id)
        {
            var token = HttpContext.Session.GetString("Token");
            var campaign = await _frCampaignService.DropCampaign(id, token);
            return View(campaign);
        }

        [HttpGet("undrop/{id}")]
        public async Task<IActionResult> UnDrop(int id)
        {
            var token = HttpContext.Session.GetString("Token");
            var campaign = await _frCampaignService.UnDropCampaign(id, token);
            return View(campaign);
        }
    }
}