using eVoucher_BUS.Requests.StaffRequests;
using eVoucher_DTO.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace eVoucher.ClientAPI_Integration
{
    public class StaffAPIClient : BaseAPIClient
    {
        private const string BASE_REQUEST = "staff";
        private readonly IConfiguration _configuration;

        public StaffAPIClient(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<Staff?> Register(StaffRegisterRequest request)
        {
            var uri = BASE_REQUEST;
            var response = await _httpClient.PostAsJsonAsync<StaffRegisterRequest>(uri, request);
            var savedstaffstring = await response.Content.ReadAsStringAsync();
            var savedstaff = JsonConvert.DeserializeObject<Staff>(savedstaffstring);
            return savedstaff;
        }
    }
}