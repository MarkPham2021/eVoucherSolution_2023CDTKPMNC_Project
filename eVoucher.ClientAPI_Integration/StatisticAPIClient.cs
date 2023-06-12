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
            
            var uri = BASE_REQUEST + "/CreatePeriodicalReport";                       
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJsonAsync(uri, request);
            var responsestring = await response.Content.ReadAsStringAsync();
            
            if (response == null)
            {
                return null;
            }
            PeriodicalReport report = JsonConvert.DeserializeObject<PeriodicalReport>(responsestring);
            return report;
        }
        public async Task<PartnerPeriodicalReport?> PartnerCreatePeriodicalReport(PartnerCreatePeriodicalReportRequest request, 
            string token)
        {
            var uri = BASE_REQUEST + "/PartnerCreatePeriodicalReport";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJsonAsync(uri, request);
            var responsestring = await response.Content.ReadAsStringAsync();
            if (response == null)
            {
                return null;
            }
            PartnerPeriodicalReport report = JsonConvert.DeserializeObject<PartnerPeriodicalReport>(responsestring);
            return report;
        }
    }
}
