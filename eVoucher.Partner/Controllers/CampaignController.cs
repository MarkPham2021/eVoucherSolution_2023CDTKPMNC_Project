﻿using eVoucher.ClientAPI_Integration;
using eVoucher_ViewModel.Requests.CampaignRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVoucher.Partner.Controllers
{
    [Authorize]
    public class CampaignController : BaseController
    {
        private CampaignAPIClient _campaignAPIClient;
        private GameAPIClient _gameAPIClient;

        public CampaignController(CampaignAPIClient campaignAPIClient, GameAPIClient gameAPIClient)
        {
            _campaignAPIClient = campaignAPIClient;
            _gameAPIClient = gameAPIClient;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("Token");
            var data = _campaignAPIClient.GetAllCampaignVMsAsync(token);
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
            var games = await _gameAPIClient.GetAllGameAsync(token);
            ViewBag.games = games;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CampaignCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var token = HttpContext.Session.GetString("Token");
            var result = await _campaignAPIClient.Create(request, token);
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