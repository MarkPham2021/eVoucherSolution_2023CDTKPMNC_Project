using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eVoucher.ClientAPI_Integration;
using eVoucher_BUS.FrontendServices;
using eVoucher_Utility.Constants;
using eVoucher_ViewModel.Requests.CustomerRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eVoucher.Client.Controllers
{
    [Authorize]
    public class CampaignController : Controller
    {
        private readonly IFrCampaignService _frCampaignService;
        private readonly IFrCustomerService _frCustomerService;
        private readonly IConfiguration _configuration;
        
        public CampaignController(IFrCampaignService frCampaignService,
                                    IFrCustomerService frCustomerService,
                                    IConfiguration configuration)
        {
            _frCampaignService = frCampaignService;
            _frCustomerService = frCustomerService;
            _configuration = configuration;
        }
        // GET: /<controller>/
        [Route("{Id}")]
        public async Task<IActionResult> CampaignDetails(int Id)
        {
            var token = HttpContext.Session.GetString("Token");
            var campaign = await _frCampaignService.GetCampaignVMById(Id, token);
            if (campaign != null)
            {
                return View("CampaignDetails", campaign);
            }

            return NotFound("Your campaign not found!");
        }

        [HttpGet]
        public async Task<IActionResult> GetVoucher(int CampaignGameId)
        {
            
            var request = new CustomerPlayGameForVoucherRequest()
            {
                AppUserInfo = User.Identity.Name,
                CampaignGameId = CampaignGameId,
                GottenNumber = new Random().Next(1, 1000)
            };

            var token = HttpContext.Session.GetString("Token");
            var response = await _frCustomerService.ClaimVoucher(request, token);
            var voucher = response._Voucher;
            return Json(voucher);
        }
    }
}

