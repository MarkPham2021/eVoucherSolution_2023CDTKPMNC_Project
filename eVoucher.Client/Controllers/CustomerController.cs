using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eVoucher.ClientAPI_Integration;
using eVoucher_BUS.Requests.CustomerRequests;
using eVoucher_BUS.Requests.UserRequests;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eVoucher.Client.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerAPIClient _customerAPIClient;
        private readonly LoginAPIClient _loginAPIClient;

        public CustomerController(CustomerAPIClient customerAPIClient,
                                  LoginAPIClient loginAPIClient)
        {
            _customerAPIClient = customerAPIClient;
            _loginAPIClient = loginAPIClient;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View("SignUp");
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
            var result = await _customerAPIClient.Register(request);

            if (result != null)
            {
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(result));

                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "Sign up failed, please try again!";

            return View("SignUp");
        }

        [HttpPost]
        public async Task<IActionResult> CustomerLogin(LoginRequest request)
        {
            var token = await _loginAPIClient.Login(request);

            if (!string.IsNullOrEmpty(token))
            {
                HttpContext.Session.SetString("LoginToken", token);

                TempData["SuccessMessage"] = "Login successfully!";

                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "Login failed due to wrong username or password, please try again!";

            return View("Login");
        }
    }
}

