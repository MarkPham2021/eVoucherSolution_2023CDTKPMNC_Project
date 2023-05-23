using eVoucher_BUS.Requests.PartnerRequests;
using eVoucher_BUS.Services;
using eVoucher_DTO.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eVoucherDatabaseWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PartnerController : ControllerBase
    {
        private IPartnerService _partnerService;
        public PartnerController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }
        
        // GET: api/<PartnerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PartnerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/partner
        [HttpPost]
        public async Task<ActionResult<Partner?>> Register([FromBody] PartnerCreateRequest request)
        {
            var result = await _partnerService.RegisterPartner(request);
            if(result==null)
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
    }
}
