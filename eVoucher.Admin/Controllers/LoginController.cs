using eShopSolution.Utilities.Constants;
using eVoucher.ClientAPI_Integration;
using eVoucher_BUS.Requests.UserRequests;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace eVoucher.Admin.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration _configuration;
        private LoginAPIClient _loginAPIClient;
        public LoginController(IConfiguration configuration, LoginAPIClient loginAPIClient)
        {
            _configuration = configuration;
            _loginAPIClient = loginAPIClient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return View(ModelState);

            var result = await _loginAPIClient.Login(request);
            if (!result.IsSucceeded)
            {
                ModelState.AddModelError("", result.Message);
                ViewData["Message"] = result.Message;
                return View();
            }
            var userPrincipal = this.ValidateToken(result.ResultObj);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = false
            };            
            HttpContext.Session.SetString(SystemConstants.AppSettings.Token, result.ResultObj);
            await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        userPrincipal,
                        authProperties);

            return RedirectToAction("Index", "Home");
        }
        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;
            SecurityToken validatedToken;
            var jwtTokentrim = jwtToken.Trim(' ','\n');
            int n = jwtToken.Length;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;
            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));            
            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtTokentrim, 
                validationParameters, out validatedToken);
            return principal;
        }
    }
}
