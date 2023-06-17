using eVoucher_BUS.Services;
using eVoucher_DTO.Models;
using eVoucher_ViewModel.Requests.StaffRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVoucherDatabaseWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        // POST api/staff
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Staff?>> Register([FromBody] StaffRegisterRequest request)
        {
            var result = await _staffService.RegisterStaff(request);
            if (result == null)
            {
                return BadRequest(null);
            }
            return Ok(result);
        }
        //Get api/staff
        [HttpGet]
        public async Task<ActionResult<List<Staff>>> GetAll()
        {
            var result = await _staffService.GetAllStaffs();
            return Ok(result);
        }
        //Get api/staff/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetSingleById([FromRoute]int id)
        {
            var staff = await _staffService.GetStaffById(id);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }
        //Get api/staff/activate/id
        [HttpGet("activate/{id}")]
        public async Task<ActionResult<Staff>> Activate(int id)
        {
            var staff = await _staffService.Activate(id);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }
        //Get api/staff/lock/id
        [HttpGet("lock/{id}")]
        public async Task<ActionResult<Staff>> Lock(int id)
        {
            var staff = await _staffService.Lock(id);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }
    }
}