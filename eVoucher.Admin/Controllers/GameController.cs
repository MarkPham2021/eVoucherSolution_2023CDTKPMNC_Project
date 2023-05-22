using eVoucher.ClientAPI_Integration;
using eVoucher_BUS.Requests.GameRequests;
using Microsoft.AspNetCore.Mvc;

namespace eVoucher.Admin.Controllers
{
    public class GameController : Controller
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
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] GameCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);            
            var result = await _gameAPIClient.Create(request);
            if (result == null)
            {
                ViewData["result"] = "unsuccess";
            }else
                ViewData["result"] = "success";
            return View(request);
        }
    }
}
