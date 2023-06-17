using eVoucher_BUS.FrontendServices;
using eVoucher_DTO.Models;
using eVoucher_Utility.Constants;
using eVoucher_ViewModel.Requests.Common;
using eVoucher_ViewModel.Requests.PartnerRequests;
using eVoucher_ViewModel.Requests.UserRequests;
using eVoucher_ViewModel.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eVoucher.Partner.Controllers
{
    [Authorize]
    public class PartnerController : Controller
    {
        private IFrPartnerService _partnerService;
        private ICommonService _commonService;
        private IConfiguration _configuration;

        public PartnerController(IFrPartnerService frPartnerService, ICommonService commonService,
            IConfiguration configuration)
        {
            _partnerService = frPartnerService;
            _commonService = commonService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Register()
        {
            string token = "abc";
            var categories = await _partnerService.GetPartnerCategoriesAsync(token);
            var selectlistpartnercategory = new List<SelectListItem>();
            foreach (PartnerCategory category in categories)
            {
                selectlistpartnercategory.Add(new SelectListItem { Text = category.Name, Value = category.Id.ToString() });
            }
            ViewBag.Categories = selectlistpartnercategory;
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetDistance(string destinations, string origins)
        {
            var request = new GetGoogleDistanceMatrixRequest()
            {
                destinations = destinations,
                origins = origins
            };
            TextValueObject response = await _commonService.GetDistanceMatrix(request);
            ViewData["result"] = response.text + $"\tvalue: {response.value}";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] PartnerCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var result = await _partnerService.Register(request);
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
        [AllowAnonymous]
        private async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return View(ModelState);

            var result = await _partnerService.Login(request);
            var userPrincipal = _commonService.ValidateToken(result.ResultObj);
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
    }
}