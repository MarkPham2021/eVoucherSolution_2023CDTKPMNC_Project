using eVoucher.ClientAPI_Integration;
using eVoucher_ViewModel.Requests.GameRequests;
using Microsoft.AspNetCore.Mvc;

namespace eVoucher.Admin.Controllers
{
    public class GameController : BaseController
    {
        private GameAPIClient _gameAPIClient;
        public GameController(GameAPIClient gameAPIClient)
        {
            _gameAPIClient = gameAPIClient;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult  Create()
        {
            return View();
        }
        [HttpPost]        
        public async Task<IActionResult> Create([FromForm] GameCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);            
            var token = HttpContext.Session.GetString("Token");
            var result = await _gameAPIClient.Create(request, token);
            if (result == null)
            {
                ViewData["result"] = "unsuccess";
            }else
                ViewData["result"] = "success";
            return View(request);
        }
    }
}
