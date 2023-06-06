using eVoucher_DAL.StatisticQuery;
using eVoucher_ViewModel.StatisticVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_BUS.BackendServices
{
    public interface IStatisticService
    {
        Task<PeriodicalReport?> CreatePeriodicalReport(CreatePeriodicalReportRequest request);
    }
    public class StatisticService: IStatisticService
    {
        private readonly PeriodicalReportQuery _periodicalReportQuery;
        public StatisticService(PeriodicalReportQuery periodicalReportQuery)
        {
            _periodicalReportQuery = periodicalReportQuery;
        }

        public async Task<PeriodicalReport?> CreatePeriodicalReport(CreatePeriodicalReportRequest request)
        {
            return await _periodicalReportQuery.GetPeriodicalReport(request);
        }
    }
}
