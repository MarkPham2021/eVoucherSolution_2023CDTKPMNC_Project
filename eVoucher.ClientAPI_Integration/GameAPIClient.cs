using eVoucher_DTO.Models;
using eVoucher_ViewModel.Requests.GameRequests;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace eVoucher.ClientAPI_Integration
{
    public class GameAPIClient : BaseAPIClient
    {
        private const string BASE_REQUEST = "game";
        private readonly IConfiguration _configuration;

        public GameAPIClient(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<Game?> Create(GameCreateRequest request, string token)
        {
            var uri = BASE_REQUEST;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJsonAsync<GameCreateRequest>(uri, request);
            var savedgamestring = await response.Content.ReadAsStringAsync();
            var savedgame = JsonConvert.DeserializeObject<Game>(savedgamestring);
            return savedgame;
        }

        public async Task<List<Game>> GetAllGameAsync(string token)
        {
            var uri = BASE_REQUEST;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetStringAsync(uri);
            var listgames = JsonConvert.DeserializeObject<List<Game>>(response);
            return listgames;
        }
    }
}