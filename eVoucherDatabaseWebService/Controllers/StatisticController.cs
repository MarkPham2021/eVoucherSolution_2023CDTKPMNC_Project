using eVoucher_BUS.BackendServices;
using eVoucher_ViewModel.StatisticVM;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost("CreatePeriodicalReport")]
        public async Task<ActionResult<PeriodicalReport>> CreatePeriodicalReport([FromBody] CreatePeriodicalReportRequest request)
        {            
            var result = await _statisticService.CreatePeriodicalReport(request);
            if (result == null)
            {
                return NotFound(null);
            }
            return Ok(result);
        }
        [HttpPost("PartnerCreatePeriodicalReport")]
        public async Task<ActionResult<PartnerPeriodicalReport>> CreatePartnerPeriodicalReport([FromBody] PartnerCreatePeriodicalReportRequest request)
        {
            var result = await _statisticService.CreatePartnerPeriodicalReport(request);
            if (result == null)
            {
                return NotFound(null);
            }
            return Ok(result);
        }
    }
}
