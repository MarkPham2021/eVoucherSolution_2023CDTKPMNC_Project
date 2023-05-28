using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eVoucher.Client.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessions;
            try
            {
                sessions = context.HttpContext.Session.GetString("Token");
            }
            catch (Exception ex)
            {
                sessions = null;
            }
            if (sessions == null)
            {
                context.Result = new RedirectToActionResult("Login", "Customer", null);
            }
            base.OnActionExecuting(context);
        }
    }
}

