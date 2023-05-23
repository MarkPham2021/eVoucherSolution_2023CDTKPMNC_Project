using eVoucher_BUS.Requests.UserRequests;
using eVoucher_BUS.Response;
using eVoucher_BUS.Services;
using Microsoft.AspNetCore.Mvc;

namespace eVoucherDatabaseWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private UserService _userService;
        public LoginController(UserService userService)
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
