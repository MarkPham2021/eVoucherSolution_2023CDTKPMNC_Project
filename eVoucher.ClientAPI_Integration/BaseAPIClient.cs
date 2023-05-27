using eVoucher_Utility.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace eVoucher.ClientAPI_Integration
{


    public class BaseAPIClient
    {
        protected const string BASE_URL = "https://localhost:7233/api/";
        protected HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public BaseAPIClient(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = _= new HttpClient();
            _httpClient.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress] + "/api/");            
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}