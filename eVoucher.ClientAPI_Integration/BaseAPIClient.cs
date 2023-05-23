using eShopSolution.Utilities.Constants;
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
        public BaseAPIClient()
        {
            _httpClient = _= new HttpClient();
            _httpClient.BaseAddress = new Uri(BASE_URL);            
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}