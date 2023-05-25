using eVoucher_BUS.Requests.CampaignRequests;
using eVoucher_BUS.Response;
using eVoucher_BUS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    }
}
