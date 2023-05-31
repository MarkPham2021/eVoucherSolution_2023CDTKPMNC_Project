using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eVoucher.ClientAPI_Integration;
using eVoucher_BUS.FrontendServices;
using eVoucher_Utility.Constants;
using eVoucher_ViewModel.Requests.CustomerRequests;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eVoucher.Client.Controllers
{
    public class CampaignController : Controller
    {
        private readonly IFrCampaignService _frCampaignService;
        private readonly IConfiguration _configuration;
        private readonly CustomerAPIClient _customerAPIClient;

        public CampaignController(IFrCampaignService frCampaignService,
                                    CustomerAPIClient customerAPIClient,
                                    IConfiguration configuration)
        {
            _frCampaignService = frCampaignService;
            _configuration = configuration;
            _customerAPIClient = customerAPIClient;
        }
        // GET: /<controller>/
        [Route("{Id}")]
        public async Task<IActionResult> CampaignDetails(int Id)
        {
            var token = HttpContext.Session.GetString("Token");
            var campaign = await _frCampaignService.GetCampaignVMById(Id, token);

            if (campaign != null)
            {
                //campaign.ImagePath = _configuration[SystemConstants.AppSettings.BaseAddress] + campaign.ImagePath;
                return View("CampaignDetails", campaign);
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
    }
}

