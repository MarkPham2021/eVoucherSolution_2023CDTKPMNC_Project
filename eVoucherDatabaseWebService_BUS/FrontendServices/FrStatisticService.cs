using Azure.Core;
using eVoucher.ClientAPI_Integration;
using eVoucher_ViewModel.StatisticVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_BUS.FrontendServices
{
    public interface IFrStatisticService
    {
        Task<PeriodicalReport?> CreatePeriodicalReport(CreatePeriodicalReportRequest request, string token);
        Task<PartnerPeriodicalReport?> PartnerCreatePeriodicalReport(PartnerCreatePeriodicalReportRequest request, string token);
    }
    public class FrStatisticService : IFrStatisticService
    {
        private readonly StatisticAPIClient _statisticAPIClient;
        public FrStatisticService(StatisticAPIClient statisticAPIClient)
        {
            _statisticAPIClient = statisticAPIClient;
        }
        public async Task<PeriodicalReport?> CreatePeriodicalReport(CreatePeriodicalReportRequest request, string token)
        {
            return await _statisticAPIClient.CreatePeriodicalReport(request,token);
        }

        public async Task<PartnerPeriodicalReport?> PartnerCreatePeriodicalReport(PartnerCreatePeriodicalReportRequest request, 
            string token)
        {
            return await _statisticAPIClient.PartnerCreatePeriodicalReport(request, token);
        }
    }
}
