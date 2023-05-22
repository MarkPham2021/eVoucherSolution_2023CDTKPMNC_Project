using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eVoucher.Admin.Controllers
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
                context.Result = new RedirectToActionResult("Login", "Staff", null);
            }
            base.OnActionExecuting(context);
        }
    }
}
