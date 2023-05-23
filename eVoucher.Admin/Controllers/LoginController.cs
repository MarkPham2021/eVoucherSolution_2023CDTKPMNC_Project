using Microsoft.AspNetCore.Mvc;

namespace eVoucher.Admin.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
