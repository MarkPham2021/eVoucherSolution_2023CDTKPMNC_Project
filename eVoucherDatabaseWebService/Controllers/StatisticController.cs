using eVoucher_BUS.BackendServices;
using eVoucher_ViewModel.StatisticVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eVoucherDatabaseWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _statisticService;
        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }
        [HttpGet("CreatePeriodicalReport/{content}")]
        public async Task<ActionResult<PeriodicalReport>> CreatePeriodicalReport([FromRoute] string content)
        {
            var request = JsonConvert.DeserializeObject<CreatePeriodicalReportRequest>(content);
            var result = await _statisticService.CreatePeriodicalReport(request);
            if (result == null)
            {
                return NotFound(null);
            }
            return Ok(result);
        }
    }
}
