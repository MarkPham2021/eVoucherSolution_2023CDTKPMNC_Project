using eVoucher_BUS.Requests.StaffRequests;
using eVoucher_BUS.Requests.UserRequests;
using eVoucher_BUS.Response;
using eVoucher_DTO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        public async Task<APIResult<string>> Login(LoginRequest request)
        {
            var uri = BASE_REQUEST;
            var response = await _httpClient.PostAsJsonAsync<LoginRequest>(uri, request);
            var responsestring = await response.Content.ReadAsStringAsync();
            var apiresult = JsonConvert.DeserializeObject<APIResult<string>>(responsestring);
            return apiresult;
        }
    }
}
