using eVoucher.ClientAPI_Integration;
using eVoucher_Utility.Constants;
using eVoucher_ViewModel.Requests.CustomerRequests;
using eVoucher_ViewModel.Requests.UserRequests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eVoucher.Client.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerAPIClient _customerAPIClient;
        private readonly LoginAPIClient _loginAPIClient;
        private IConfiguration _configuration;

        public CustomerController(CustomerAPIClient customerAPIClient,
                                  LoginAPIClient loginAPIClient,
                                  IConfiguration configuration)
        {
            _customerAPIClient = customerAPIClient;
            _loginAPIClient = loginAPIClient;
            _configuration = configuration;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterNewCustomer(CustomerRegisterRequest request)
        {
            request.CreatedBy = "Self";
            request.CreatedTime = DateTime.Now;
            var result = await _customerAPIClient.Register(request);

            if (result != null)
            {
                var login = new LoginRequest()
                {
                    UserName = request.UserName,
                    Password = request.Password,
                    UserTypeId = request.UserTypeId,
                    Rememberme = false
                };

                await CustomerLogin(login);
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "Sign up failed, please try again!";

            return View("SignUp");
        }

        [HttpPost]
        public async Task<IActionResult> CustomerLogin(LoginRequest request)
        {
            var result = await _loginAPIClient.Login(request);

            if (result !=null && result.IsSucceeded)
            {
                //HttpContext.Session.SetString("LoginToken", result.ResultObj);

                TempData["SuccessMessage"] = "Login successfully!";
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

            TempData["ErrorMessage"] = "Login failed due to wrong username or password, please try again!";

            return View("Login");
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