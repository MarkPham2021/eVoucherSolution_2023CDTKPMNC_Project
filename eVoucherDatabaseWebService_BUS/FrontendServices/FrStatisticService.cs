using eVoucher.ClientAPI_Integration;
using eVoucher_ViewModel.StatisticVM;

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
            return await _statisticAPIClient.CreatePeriodicalReport(request, token);
        }

        public async Task<PartnerPeriodicalReport?> PartnerCreatePeriodicalReport(PartnerCreatePeriodicalReportRequest request,
            string token)
        {
            return await _statisticAPIClient.PartnerCreatePeriodicalReport(request, token);
        }
    }
}