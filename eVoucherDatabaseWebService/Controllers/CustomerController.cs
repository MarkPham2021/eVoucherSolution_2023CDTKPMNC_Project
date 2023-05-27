﻿using eVoucher_BUS.Services;
using eVoucher_DTO.Models;
using eVoucher_ViewModel.Requests.CustomerRequests;
using Microsoft.AspNetCore.Mvc;

namespace eVoucherDatabaseWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // POST api/<StaffController>
        [HttpPost]
        public async Task<ActionResult<Customer?>> Register([FromBody] CustomerRegisterRequest request)
        {
            var result = await _customerService.RegisterCustomer(request);
            if (result == null)
            {
                return BadRequest(null);
            }
            return Ok(result);
        }
    }
}