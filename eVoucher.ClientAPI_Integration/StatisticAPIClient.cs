using eVoucher_ViewModel.Requests.VoucherRequests;
using eVoucher_ViewModel.StatisticVM;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher.ClientAPI_Integration
{
    public class StatisticAPIClient: BaseAPIClient
    {
        private const string BASE_REQUEST = "Statistic";
        private readonly IConfiguration _configuration;

        public StatisticAPIClient(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }
        public async Task<PeriodicalReport?> CreatePeriodicalReport(CreatePeriodicalReportRequest request, string token)
        {
            string content = JsonConvert.SerializeObject(request);
            var uri = BASE_REQUEST + $"/CreatePeriodicalReport/{content}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetStringAsync(uri);
            if (response == null)
            {
                return null;
            }
            PeriodicalReport report = JsonConvert.DeserializeObject<PeriodicalReport>(response);
            return report;
        }
    }
}
