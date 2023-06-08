using eVoucher_BUS.FrontendServices;
using eVoucher_BUS.Services;
using eVoucher_DTO.Models;
using eVoucher_ViewModel.Requests.CampaignRequests;
using eVoucher_ViewModel.Requests.PartnerRequests;
using eVoucher_ViewModel.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVoucherDatabaseWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CampaignController : ControllerBase
    {
        private ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        // POST api/campaign/create
        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<APIResult<string>>> Create([FromForm] CampaignCreateRequestforBackend request)
        {
            var result = await _campaignService.CreateCampaign(request);
            if (!result.IsSucceeded)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("createvouchertype")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<APIResult<string>>> CreateVoucherType([FromForm] CampaignCreateVoucherTypeRequest request)
        {
            var apiresult = await _campaignService.CreateCampaignVoucherType(request);
            if (!apiresult.IsSucceeded) { return BadRequest(apiresult); }
            return Ok(apiresult);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<CampaignVM>>> GetAllCampaignVMs()
        {
            var CampaignList = await _campaignService.GetAllCampaignVMs();
            return Ok(CampaignList);
        }
        // GET api/campaign/drop/5
        [HttpGet("drop/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Campaign?>> LockPartner(int id)
        {
            return await _campaignService.DropCampaign(id);
        }
        // GET api/campaign/undrop/5
        [HttpGet("undrop/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Campaign?>> UnLockPartner(int id)
        {
            return await _campaignService.UnDropCampaign(id);
        }
    }
}