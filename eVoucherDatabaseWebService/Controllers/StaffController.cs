using eVoucher_BUS.Requests.StaffRequests;
using eVoucher_BUS.Services;
using eVoucher_DTO.Models;
using Microsoft.AspNetCore.Mvc;

namespace eVoucherDatabaseWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : Controller
    {
        private StaffService _staffService;

        public StaffController(StaffService staffService)
        {
            _staffService = staffService;
        }

        // POST api/<StaffController>
        [HttpPost]
        public async Task<ActionResult<Staff?>> Register([FromBody] StaffRegisterRequest request)
        {
            
            var result = await _staffService.RegisterStaff(request);
            if(result==null)
            {
                return BadRequest(null);
            }
            return Ok(result);
        }
    }
}