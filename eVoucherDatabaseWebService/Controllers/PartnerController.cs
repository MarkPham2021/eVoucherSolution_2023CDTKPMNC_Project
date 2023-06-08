using eVoucher_BUS.Services;
using eVoucher_DTO.Models;
using eVoucher_ViewModel.Requests.PartnerRequests;
using eVoucher_ViewModel.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eVoucherDatabaseWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private IPartnerService _partnerService;
        private IPartnerCategoryService _partnerCategoryService;

        public PartnerController(IPartnerService partnerService, IPartnerCategoryService partnerCategoryService)
        {
            _partnerService = partnerService;
            _partnerCategoryService = partnerCategoryService;
        }

        // GET: api/<PartnerController>
        [HttpGet("getallpartnercategories")]
        public async Task<ActionResult<List<PartnerCategory>>> GetAllPartnerCategories()
        {
            //var categories = _partnerCategoryService.GetAllPartnerCategorys().ToList();
            var categories = await _partnerCategoryService.GetAllPartnerCategoriesAsync();
            return Ok(categories);
        }

        // GET api/partner/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PartnerVM?>> Get(int id)
        {
            return await _partnerService.GetPartnerById(id);
        }
        // GET api/partner
        [HttpGet]
        public async Task<ActionResult<List<Partner>>> Get()
        {
            return await _partnerService.GetAllPartners();
        }
        // POST api/partner/register
        [HttpPost("register")]
        [Consumes("multipart/form-data")]
        [AllowAnonymous]
        public async Task<ActionResult<APIResult<string>>> Register([FromForm] PartnerCreateRequest request)
        {
            var result = await _partnerService.RegisterPartner(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // PUT api/<PartnerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PartnerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        // GET api/partner/lock/5
        [HttpGet("lock/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PartnerVM?>> LockPartner(int id)
        {
            return await _partnerService.LockPartner(id);
        }
        // GET api/partner/unlock/5
        [HttpGet("unlock/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PartnerVM?>> UnLockPartner(int id)
        {
            return await _partnerService.UnLockPartner(id);
        }
    }
}