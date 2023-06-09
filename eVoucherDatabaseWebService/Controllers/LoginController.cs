﻿using eVoucher_BUS.Services;
using eVoucher_ViewModel.Requests.UserRequests;
using eVoucher_ViewModel.Response;
using Microsoft.AspNetCore.Mvc;

namespace eVoucherDatabaseWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<APIResult<string>>> Login([FromBody] LoginRequest request)
        {
            var result = await _userService.Authenticate(request);
            if (!result.IsSucceeded)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}