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
        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<ActionResult<List<Customer>?>> GetAllCustomersFullInfo()
        {
            var data = await _customerService.GetAllCustomersFullInfo();
            return Ok(data);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Customer?>> GetById(int id)
        {
            var customer = await _customerService.GetCustomerFullInfoById(id);
            return Ok(customer);
        }
        [HttpGet("userinfo=",Name = "userinfo")]
        [AllowAnonymous]
        public async Task<ActionResult<Customer?>> GetById(string userinfo)
        {
            var customer = await _customerService.GetCustomerFullInfoByUserInfo(userinfo);
            return Ok(customer);
        }
        [HttpGet("GetAllVouchersOfCustomerByUserInfo/{userinfo}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<VoucherVM>?>> GetAllVouchersOfCustomerByUserInfo(string userinfo)
        {
            var vouchervms = await _customerService.GetAllVouchersOfCustomerByUserInfo(userinfo);
            return Ok(vouchervms);
        }
        //GetAllVoucherVMsOfCustomerByCustomerId(int id)
        [HttpGet("GetAllVoucherVMsOfCustomerByCustomerId/{customerid}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<VoucherVM>?>> GetAllVoucherVMsOfCustomerByCustomerId(int customerid)
        {
            var vouchervms = await _customerService.GetAllVouchersOfCustomerByCustomerId(customerid);
            return Ok(vouchervms);
        }
    }
}