using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVoucher.Partner.Controllers
{
    [Authorize]
    public class VoucherTypeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
