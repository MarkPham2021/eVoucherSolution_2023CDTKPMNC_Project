using eVoucher_Utility.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using eVoucher_ViewModel.Requests.Common;
using eVoucher_ViewModel.Requests.CampaignRequests;
using Newtonsoft.Json;
using eVoucher_ViewModel.Response;

namespace eVoucher.ClientAPI_Integration
{
    public class GoogleDistanceMatrixAPICLient
    {
        const string BASE_URL = "https://maps.googleapis.com/maps/api/distancematrix/json";
        const string GOOGLEKEY = "AIzaSyCVoYSMkFNbyU31-aDYJnNsoF5ky36Ydvk";
        private HttpClient _httpClient;
        public GoogleDistanceMatrixAPICLient() 
        {
            _httpClient = _ = new HttpClient();
            _httpClient.BaseAddress = new Uri(BASE_URL);            
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<TextValueObject> GetDistanceMatrix(GetGoogleDistanceMatrixRequest request)
        {
            
            request.key = GOOGLEKEY;
            string requestURI = $"?destinations={request.destinations}&origins={request.origins} &language=vi &key={request.key}";
            var response = await _httpClient.GetStringAsync(requestURI);
            TextValueObject result = new TextValueObject();
            DistanceMatrixAPIResponse distanceMatrixAPIResponse = 
                JsonConvert.DeserializeObject<DistanceMatrixAPIResponse>(response);
            if(distanceMatrixAPIResponse.status.ToString().ToLower() == "ok" ) 
            {
                result = distanceMatrixAPIResponse.rows[0].elements[0].distance;
            }            
            return result;
        }
    }
}
