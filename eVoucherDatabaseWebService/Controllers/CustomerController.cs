using eVoucher_BUS.FrontendServices;
using eVoucher_BUS.Services;
using eVoucher_DTO.Models;
using eVoucher_ViewModel.Requests.CustomerRequests;
using eVoucher_ViewModel.Requests.VoucherRequests;
using eVoucher_ViewModel.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVoucherDatabaseWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // POST api/<StaffController>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Customer?>> Register([FromBody] CustomerRegisterRequest request)
        {
            var result = await _customerService.RegisterCustomer(request);
            if (result == null)
            {
                return BadRequest(null);
            }
            return Ok(result);
        }
        [HttpPost("ClaimVoucher")]
        public async Task<ActionResult<APIClaimVoucherResult>> ClaimVoucher([FromBody] CustomerPlayGameForVoucherRequest request)
        {
            var result = await _customerService.ClaimVoucher(request);
            if (result == null)
            {
                return BadRequest(null);
            }
            return Ok(result);
        }
        [HttpGet]        
        public async Task<ActionResult<List<Customer>?>> GetAllCustomersFullInfo()
        {
            var data = await _customerService.GetAllCustomersFullInfo();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer?>> GetById(int id)
        {
            var customer = await _customerService.GetCustomerFullInfoById(id);
            return Ok(customer);
        }
        [HttpGet("userinfo/{userinfo}")]
        public async Task<ActionResult<Customer?>> GetById(string userinfo)
        {
            var customer = await _customerService.GetCustomerFullInfoByUserInfo(userinfo);
            return Ok(customer);
        }
        [HttpGet("GetAllVouchersOfCustomerByUserInfo/{userinfo}")]
        public async Task<ActionResult<List<VoucherVM>?>> GetAllVouchersOfCustomerByUserInfo(string userinfo)
        {
            var vouchervms = await _customerService.GetAllVouchersOfCustomerByUserInfo(userinfo);
            return Ok(vouchervms);
        }
        
        [HttpGet("GetAllVoucherVMsOfCustomerByCustomerId/{customerid}")]
        public async Task<ActionResult<List<VoucherVM>?>> GetAllVoucherVMsOfCustomerByCustomerId(int customerid)
        {
            var vouchervms = await _customerService.GetAllVouchersOfCustomerByCustomerId(customerid);
            return Ok(vouchervms);
        }
        
        [HttpGet("GetVoucherVMById/{id}")]
        public async Task<ActionResult<VoucherVM?>> GetVoucherVMById(int id)
        {
            var vouchervm = await _customerService.GetVoucherVMById(id);
            return Ok(vouchervm);
        }
        //Get api/staff/activate/id
        [HttpGet("activate/{id}")]
        public async Task<ActionResult<Staff>> Activate(int id)
        {
            var staff = await _customerService.Activate(id);
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
            var staff = await _customerService.Lock(id);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }
    }
}