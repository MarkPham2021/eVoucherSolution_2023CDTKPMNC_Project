using eVoucher_BUS.Requests.GameRequests;
using eVoucher_BUS.Requests.StaffRequests;
using eVoucher_DTO.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher.ClientAPI_Integration
{
    public class StaffAPIClient : BaseAPIClient
    {
        const string BASE_REQUEST = "staff";
        public StaffAPIClient() : base() { }
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
