using eVoucher_DTO.Models;
using eVoucher_ViewModel.Requests.CustomerRequests;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace eVoucher.ClientAPI_Integration
{
    public class CustomerAPIClient : BaseAPIClient
    {
        private const string BASE_REQUEST = "Customer";
        private readonly IConfiguration _configuration;

        public CustomerAPIClient(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<Customer?> Register(CustomerRegisterRequest request)
        {
            var uri = BASE_REQUEST + "/Register";
            var response = await _httpClient.PostAsJsonAsync<CustomerRegisterRequest>(uri, request);
            var savedcustomerstring = await response.Content.ReadAsStringAsync();
            var savedcustomer = JsonConvert.DeserializeObject<Customer>(savedcustomerstring);
            return savedcustomer;
        }
    }
}