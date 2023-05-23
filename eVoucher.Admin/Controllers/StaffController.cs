using eVoucher.ClientAPI_Integration;
using eVoucher_BUS.Requests.StaffRequests;
using Microsoft.AspNetCore.Mvc;

namespace eVoucher.Admin.Controllers
{
    public class StaffController : Controller
    {
        private StaffAPIClient _staffAPIClient;
        public StaffController(StaffAPIClient staffAPIClient)
        {
            _staffAPIClient = staffAPIClient;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm]StaffRegisterRequest request)
        {
            
            if (!ModelState.IsValid)
                return View(request);            
            var result = await _staffAPIClient.Register(request);
            if (result == null)
            {
                ViewData["result"] = "unsuccess";
            }
            else
                ViewData["result"] = "success";
            return View(request);
            
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }
    }
}
