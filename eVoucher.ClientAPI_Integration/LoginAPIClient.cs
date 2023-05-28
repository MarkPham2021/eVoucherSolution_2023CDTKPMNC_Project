using eVoucher_ViewModel.Requests.UserRequests;
using eVoucher_ViewModel.Response;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace eVoucher.ClientAPI_Integration
{
    public class LoginAPIClient : BaseAPIClient
    {
        private const string BASE_REQUEST = "login";
        private readonly IConfiguration _configuration;

        public LoginAPIClient(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<APIResult<string>?> Login(LoginRequest request)
        {
            var uri = BASE_REQUEST;
            var response = await _httpClient.PostAsJsonAsync<LoginRequest>(uri, request);
            var responsestring = await response.Content.ReadAsStringAsync();
            var apiresult = JsonConvert.DeserializeObject<APIResult<string>>(responsestring);
            return apiresult;
        }
    }
}