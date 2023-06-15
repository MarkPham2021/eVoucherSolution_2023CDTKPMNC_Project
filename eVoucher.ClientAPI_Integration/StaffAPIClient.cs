using eVoucher_DTO.Models;
using eVoucher_ViewModel.Requests.StaffRequests;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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
        public async Task<List<Staff>> GetAll(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetStringAsync(BASE_REQUEST);
            var staffs = JsonConvert.DeserializeObject<List<Staff>>(response);
            return staffs;
        }
        public async Task<Staff> GetSingleById(int id, string token)
        {
            var uri = BASE_REQUEST + $"/{id}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetStringAsync(uri);
            var staff = JsonConvert.DeserializeObject<Staff>(response);
            return staff;
        }
        public async Task<Staff> Activate(int id, string token)
        {
            var uri = BASE_REQUEST + $"/activate/{id}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetStringAsync(uri);
            var staff = JsonConvert.DeserializeObject<Staff>(response);
            return staff;
        }
        public async Task<Staff> Lock(int id, string token)
        {
            var uri = BASE_REQUEST + $"/lock/{id}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetStringAsync(uri);
            var staff = JsonConvert.DeserializeObject<Staff>(response);
            return staff;
        }
    }
}