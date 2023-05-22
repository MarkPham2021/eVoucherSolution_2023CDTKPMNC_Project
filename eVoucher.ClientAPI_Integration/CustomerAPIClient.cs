using eVoucher_BUS.Requests.CustomerRequests;
using eVoucher_BUS.Requests.StaffRequests;
using eVoucher_DTO.Models;
using Newtonsoft.Json;

namespace eVoucher.ClientAPI_Integration
{
    public class CustomerAPIClient : BaseAPIClient
    {
        private const string BASE_REQUEST = "customer";

        public CustomerAPIClient() : base()
        {
        }

        public async Task<Customer?> Register(CustomerRegisterRequest request)
        {
            var uri = BASE_REQUEST;
            var response = await _httpClient.PostAsJsonAsync<CustomerRegisterRequest>(uri, request);
            var savedcustomerstring = await response.Content.ReadAsStringAsync();
            var savedcustomer = JsonConvert.DeserializeObject<Customer>(savedcustomerstring);
            return savedcustomer;
        }
    }
}