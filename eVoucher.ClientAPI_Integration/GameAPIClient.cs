using Azure.Core;
using eShopSolution.Utilities.Constants;
using eVoucher_BUS.Requests.GameRequests;
using eVoucher_DTO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher.ClientAPI_Integration
{
    public class GameAPIClient : BaseAPIClient
    {
        const string BASE_REQUEST = "game";
        
        public GameAPIClient() : base() { }
        public async Task<Game?> Create(GameCreateRequest request, string token)
        {
            var uri = BASE_REQUEST;            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token) ;
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
            var listgames = JsonConvert.DeserializeObject < List<Game>>(response);            
            return listgames;
        }
    }
}
