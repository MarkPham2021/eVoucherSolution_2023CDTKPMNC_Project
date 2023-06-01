using eVoucher_BUS.FrontendServices;
using eVoucher_ViewModel.Requests.VoucherRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVoucher.Client.Controllers
{
    [Authorize]
    public class VoucherController : Controller
    {
        private IFrCustomerService _customerService;
        public VoucherController(IFrCustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword = "", int pageIndex = 1, int pageSize = 4)
        {
            var token = HttpContext.Session.GetString("Token");
            var request = new GetCustomerVouchersRequestPaging()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            string userinfo = User.Identity.Name;
            var data = await _customerService.GetCustomerVouchersPaging(userinfo, request, token);
            return View(data);
        }
    }
}