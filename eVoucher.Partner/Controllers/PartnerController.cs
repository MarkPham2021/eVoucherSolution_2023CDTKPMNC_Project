using eVoucher.ClientAPI_Integration;
using eVoucher_BUS.Requests.PartnerRequests;
using eVoucher_BUS.Requests.StaffRequests;
using eVoucher_DTO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace eVoucher.Partner.Controllers
{
    
    public class PartnerController : Controller
    {
        private PartnerAPIClient _partnerapiclient;
        public PartnerController(PartnerAPIClient partnerapiclient)
        {
            _partnerapiclient = partnerapiclient;
        }

        public IActionResult Index()
        {
            
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Register()
        {
            var categories = await _partnerapiclient.GetAllPartnerCategoriesAsync();
            var selectlistpartnercategory = new List<SelectListItem>();
            foreach (PartnerCategory category in categories)
            {
                selectlistpartnercategory.Add(new SelectListItem { Text = category.Name, Value = category.Id.ToString() });
            }
            ViewBag.Categories = selectlistpartnercategory;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] PartnerCreateRequest request)
        {

            if (!ModelState.IsValid)
                return View(request);
            var result = await _partnerapiclient.Register(request);
            if (!result.IsSucceeded)
            {
                ViewData["result"] = "unsuccess";
            }
            else
                ViewData["result"] = "success";
            return View(request);

        }
    }
}
