using eVoucher_BUS.FrontendServices;
using eVoucher_Utility.Enums;
using eVoucher_ViewModel.Requests.CustomerRequests;
using eVoucher_ViewModel.Requests.StaffRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Drawing.Printing;

namespace eVoucher.Admin.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly IFrCustomerService _frcustomerService;
        public CustomerController(IFrCustomerService frCustomerService) 
        {
            _frcustomerService = frCustomerService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string keyword = "",  int pageIndex = 1, int pageSize = 10, ActiveStatus accountStatus = ActiveStatus.AllStatus)
        {
            var token = HttpContext.Session.GetString("Token");
            var request = new GetAllCustomersPagingRequest()
            {
                Keyword = keyword,
                AccountStatus = accountStatus,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var pageresult = await _frcustomerService.GetAdminAllCustomersPaging(request, token);
            return View(pageresult);
        }

        [HttpGet()]
        public async Task<IActionResult> ViewDetail(int id)
        {
            var token = HttpContext.Session.GetString("Token");
            var customer = await _frcustomerService.GetCustomerFullInfoById(id, token);
            return View(customer);
        }
        
        [HttpGet("Customer/Activate/{id}")]
        public async Task<IActionResult> Activate(int id)
        {
            var token = HttpContext.Session.GetString("Token");
            var staff = await _frcustomerService.Activate(id, token);
            var request = new GetAllCustomersPagingRequest()
            {
                Keyword = "",
                AccountStatus = ActiveStatus.AllStatus,
                PageIndex = 1,
                PageSize = 10
            };
            var pageresult = await _frcustomerService.GetAdminAllCustomersPaging(request, token);
            return View("Index", pageresult);
        }

        [HttpGet("Customer/Lock/{id}")]
        public async Task<IActionResult> Lock(int id)
        {
            var token = HttpContext.Session.GetString("Token");
            var staff = await _frcustomerService.Lock(id, token);
            var request = new GetAllCustomersPagingRequest()
            {
                Keyword = "",
                AccountStatus = ActiveStatus.AllStatus,
                PageIndex = 1,
                PageSize = 10
            };
            var pageresult = await _frcustomerService.GetAdminAllCustomersPaging(request, token);
            return View("Index", pageresult);
        }
        
    }
}
