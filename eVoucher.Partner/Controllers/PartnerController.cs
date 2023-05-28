using eVoucher.ClientAPI_Integration;
using eVoucher_DTO.Models;
using eVoucher_Utility.Constants;
using eVoucher_ViewModel.Requests.PartnerRequests;
using eVoucher_ViewModel.Requests.UserRequests;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eVoucher.Partner.Controllers
{
    public class PartnerController : Controller
    {
        private PartnerAPIClient _partnerapiclient;
        private LoginAPIClient _loginapiclient;
        private IConfiguration _configuration;

        public PartnerController(PartnerAPIClient partnerapiclient, LoginAPIClient loginapiclient, IConfiguration configuration)
        {
            _partnerapiclient = partnerapiclient;
            _loginapiclient = loginapiclient;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Register()
        {
            var categories = await _partnerapiclient.GetAllPartnerCategoriesAsync();
            var selectlistpartnercategory = new List<SelectListItem>();
            foreach (PartnerCategory category in categories)
            {
                selectlistpartnercategory.Add(new SelectListItem { Text = category.Name, Value = category.Id.ToString() });
            }
            ViewBag.Categories = selectlistpartnercategory;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] PartnerCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var result = await _partnerapiclient.Register(request);
            if (result != null)
            {
                var login = new LoginRequest()
                {
                    UserName = request.UserName,
                    Password = request.Password,
                    UserTypeId = request.UserTypeId,
                    Rememberme = false
                };

                await Login(login);
                return RedirectToAction("Index", "Home");
            }
            ViewData["result"] = result.Message;
            return View(request);
        }
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return View(ModelState);

            var result = await _loginapiclient.Login(request);            
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
            var jwtTokentrim = jwtToken.Trim(' ', '\n');
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