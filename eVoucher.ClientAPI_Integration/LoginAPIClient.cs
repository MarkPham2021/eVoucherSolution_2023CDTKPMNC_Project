using eVoucher_BUS.Requests.StaffRequests;
using eVoucher_BUS.Requests.UserRequests;
using eVoucher_DTO.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher.ClientAPI_Integration
{
    public class LoginAPIClient : BaseAPIClient
    {
        const string BASE_REQUEST = "login";
        public LoginAPIClient() : base() { }
        public async Task<string?> Login(LoginRequest request)
        {
            var uri = BASE_REQUEST;
            var response = await _httpClient.PostAsJsonAsync<LoginRequest>(uri, request);
            var tokenstring = await response.Content.ReadAsStringAsync();            
            return tokenstring;
        }
    }
}
