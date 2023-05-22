using eVoucher_BUS.Requests.GameRequests;
using eVoucher_DTO.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher.ClientAPI_Integration
{
    public class GameAPIClient : BaseAPIClient
    {
        const string BASE_REQUEST = "game";
        public GameAPIClient(): base() { }
        public async Task<Game?> Create(GameCreateRequest request)
        {
            var uri = BASE_REQUEST;
            var response = await _httpClient.PostAsJsonAsync<GameCreateRequest>(uri, request);
            var savedgamestring = await response.Content.ReadAsStringAsync();
            var savedgame = JsonConvert.DeserializeObject<Game>(savedgamestring);
            return savedgame;
        }
    }
}
