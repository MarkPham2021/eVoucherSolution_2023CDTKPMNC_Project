﻿using eVoucher_BUS.FrontendServices;
using eVoucher_ViewModel.Requests.StaffRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVoucher.Admin.Controllers
{
    [Authorize]
    public class StaffController : Controller
    {
        private readonly IFrStaffService _staffService;

        public StaffController(IFrStaffService frStaffService)
        {
            _staffService = frStaffService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword = "", int pageIndex = 1, int pageSize = 10)
        {
            var token = HttpContext.Session.GetString("Token");
            var request = new GetAllStaffPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var pageresult = await _staffService.GetAllStaffPaging(request, token);
            return View(pageresult);
        }

        [HttpGet()]
        public async Task<IActionResult> ViewDetail(int id)
        {
            var token = HttpContext.Session.GetString("Token");
            var staff = await _staffService.GetSingleById(id, token);
            return View(staff);
        }

        [HttpGet("staff/Activate/{id}")]
        public async Task<IActionResult> Activate(int id)
        {
            var token = HttpContext.Session.GetString("Token");
            var staff = await _staffService.Activate(id, token);
            var request = new GetAllStaffPagingRequest()
            {
                Keyword = "",
                PageIndex = 1,
                PageSize = 10
            };
            var pageresult = await _staffService.GetAllStaffPaging(request, token);
            return View("Index", pageresult);
        }

        [HttpGet("staff/Lock/{id}")]
        public async Task<IActionResult> Lock(int id)
        {
            var token = HttpContext.Session.GetString("Token");
            var staff = await _staffService.Lock(id, token);
            var request = new GetAllStaffPagingRequest()
            {
                Keyword = "",
                PageIndex = 1,
                PageSize = 10
            };
            var pageresult = await _staffService.GetAllStaffPaging(request, token);
            return View("Index", pageresult);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] StaffRegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var result = await _staffService.Register(request);
            if (result == null)
            {
                ViewData["result"] = "unsuccess register, please check your information and try again.";
            }
            else
                ViewData["result"] = "Your account has been registered successfully, please wait for admin activating.";
            return View(request);
        }
    }
}