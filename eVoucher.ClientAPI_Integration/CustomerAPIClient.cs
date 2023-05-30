using eVoucher_DTO.Models;
using eVoucher_ViewModel.Requests.CustomerRequests;
using eVoucher_ViewModel.Response;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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
            var uri = BASE_REQUEST;
            var response = await _httpClient.PostAsJsonAsync<CustomerRegisterRequest>(uri, request);
            var savedcustomerstring = await response.Content.ReadAsStringAsync();
            var savedcustomer = JsonConvert.DeserializeObject<Customer>(savedcustomerstring);
            return savedcustomer;
        }
        public async Task<APIClaimVoucherResult> ClaimVoucher(CustomerPlayGameForVoucherRequest request, string token)
        {
            var uri = BASE_REQUEST + "/claimvoucher";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJsonAsync<CustomerPlayGameForVoucherRequest>(uri, request);
            var apiclaimvoucherresultstring = await response.Content.ReadAsStringAsync();
            var apiclaimvoucherresult = JsonConvert.DeserializeObject<APIClaimVoucherResult>(apiclaimvoucherresultstring);
            return apiclaimvoucherresult;

        }
        public async Task<Customer?> GetSingleCustomerAllInfoByUserInfo(string userinfo, string token)
        {
            var uri = BASE_REQUEST + $"?userinfo={userinfo}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetStringAsync(uri);
            Customer customer = JsonConvert.DeserializeObject<Customer>(response);
            return customer;
        }
        public async Task<Customer?> GetSingleCustomerAllInfoById(int id, string token)
        {
            var uri = BASE_REQUEST + $"/{id}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetStringAsync(uri);
            Customer customer = JsonConvert.DeserializeObject<Customer>(response);
            return customer;
        }
    }
}