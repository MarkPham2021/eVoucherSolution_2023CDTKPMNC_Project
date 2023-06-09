﻿using eVoucher_DTO.Models;
using eVoucher_ViewModel.Requests.CampaignRequests;
using eVoucher_ViewModel.Requests.PartnerRequests;
using eVoucher_ViewModel.Response;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace eVoucher.ClientAPI_Integration
{
    public class CampaignAPIClient : BaseAPIClient
    {
        private const string BASE_REQUEST = "campaign";
        private readonly IConfiguration _configuration;

        public CampaignAPIClient(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<CampaignVM>?> GetAllCampaignVMsAsync(string token)
        {
            var uri = BASE_REQUEST;
            //uri: ROOTPATH/campaign
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetStringAsync(uri);
            var campaigns = JsonConvert.DeserializeObject<List<CampaignVM>>(response);
            return campaigns;
        }

        public async Task<APIResult<string>> CreateNotAssignGames(CampaignCreateRequest request, string token)
        {
            var uri = BASE_REQUEST + "/create";

            var requestContent = new MultipartFormDataContent();
            if (request.ImageFile != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ImageFile.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ImageFile.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ImageFile", $"{request.Name}thumbnailImage{request.ImageFile.FileName}");
            }
            requestContent.Add(new StringContent(request.Name), "Name");
            requestContent.Add(new StringContent(request.Slogan), "Slogan");
            requestContent.Add(new StringContent(request.PartnerAppUserId.ToString()), "PartnerAppUserId");
            requestContent.Add(new StringContent(request.MetaKeyword), "MetaKeyword");
            requestContent.Add(new StringContent(request.MetaDescription), "MetaDescription");
            requestContent.Add(new StringContent(request.HomeFlag.ToString()), "HomeFlag");
            requestContent.Add(new StringContent(request.HotFlag.ToString()), "HotFlag");
            requestContent.Add(new StringContent(request.BeginningDate.ToString()), "BeginningDate");
            requestContent.Add(new StringContent(request.EndingDate.ToString()), "EndingDate");
            requestContent.Add(new StringContent(request.CreatedBy), "CreatedBy");
            requestContent.Add(new StringContent(request.CreatedTime.ToString()), "CreatedTime");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            var response = await _httpClient.PostAsync(uri, requestContent);

            var responsestring = await response.Content.ReadAsStringAsync();
            var apiresult = JsonConvert.DeserializeObject<APIResult<string>>(responsestring);
            return apiresult;
        }

        public async Task<APIResult<string>> Create(CampaignCreateRequest request, string token)
        {
            var uri = BASE_REQUEST + "/create";
            string selectitem = JsonConvert.SerializeObject(request.Games);
            var requestContent = new MultipartFormDataContent();
            if (request.ImageFile != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ImageFile.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ImageFile.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ImageFile", $"{request.Name}thumbnailImage{request.ImageFile.FileName}");
            }
            requestContent.Add(new StringContent(selectitem), "Games");
            requestContent.Add(new StringContent(request.Name), "Name");
            requestContent.Add(new StringContent(request.Slogan), "Slogan");
            requestContent.Add(new StringContent(request.PartnerAppUserId.ToString()), "PartnerAppUserId");
            requestContent.Add(new StringContent(request.MetaKeyword), "MetaKeyword");
            requestContent.Add(new StringContent(request.MetaDescription), "MetaDescription");
            requestContent.Add(new StringContent(request.HomeFlag.ToString()), "HomeFlag");
            requestContent.Add(new StringContent(request.HotFlag.ToString()), "HotFlag");
            requestContent.Add(new StringContent(request.BeginningDate.ToString()), "BeginningDate");
            requestContent.Add(new StringContent(request.EndingDate.ToString()), "EndingDate");
            requestContent.Add(new StringContent(request.CreatedBy), "CreatedBy");
            requestContent.Add(new StringContent(request.CreatedTime.ToString()), "CreatedTime");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            var response = await _httpClient.PostAsync(uri, requestContent);

            var responsestring = await response.Content.ReadAsStringAsync();
            var apiresult = JsonConvert.DeserializeObject<APIResult<string>>(responsestring);
            return apiresult;
        }
        public async Task<APIResult<string>> Edit(CampaignEditRequest request, string token)
        {
            var uri = BASE_REQUEST + "/edit";
            string selectitem = JsonConvert.SerializeObject(request.Games);
            var requestContent = new MultipartFormDataContent();
            if (request.ImageFile != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ImageFile.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ImageFile.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ImageFile", $"{request.Name}thumbnailImage{request.ImageFile.FileName}");
            }
            requestContent.Add(new StringContent(selectitem), "Games");
            requestContent.Add(new StringContent(request.Id.ToString()), "Id");
            requestContent.Add(new StringContent(request.Name), "Name");
            requestContent.Add(new StringContent(request.Slogan), "Slogan");           
            requestContent.Add(new StringContent(request.MetaKeyword), "MetaKeyword");
            requestContent.Add(new StringContent(request.MetaDescription), "MetaDescription");
            requestContent.Add(new StringContent(request.HomeFlag.ToString()), "HomeFlag");
            requestContent.Add(new StringContent(request.HotFlag.ToString()), "HotFlag");
            requestContent.Add(new StringContent(request.BeginningDate.ToString()), "BeginningDate");
            requestContent.Add(new StringContent(request.EndingDate.ToString()), "EndingDate");
            requestContent.Add(new StringContent(request.UpdatedBy), "UpdatedBy");
            requestContent.Add(new StringContent(request.UpdatedTime.ToString()), "UpdatedTime");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            var response = await _httpClient.PutAsync(uri, requestContent);

            var responsestring = await response.Content.ReadAsStringAsync();
            var apiresult = JsonConvert.DeserializeObject<APIResult<string>>(responsestring);
            return apiresult;
        }
        public async Task<APIResult<string>> CreateVoucherType(CampaignCreateVoucherTypeRequest request, string token)
        {
            var uri = BASE_REQUEST + "/createvouchertype";           
            var requestContent = new MultipartFormDataContent();
            if (request.ImageFile != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ImageFile.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ImageFile.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ImageFile", $"{request.Name}thumbnailImage{request.ImageFile.FileName}");
            }
            requestContent.Add(new StringContent(request.Name), "Name");            
            requestContent.Add(new StringContent(request.DiscountRate.ToString()), "DiscountRate");
            requestContent.Add(new StringContent(request.CampaignId.ToString()), "CampaignId");
            requestContent.Add(new StringContent(request.Promotion), "Promotion");
            requestContent.Add(new StringContent(request.MaxAmount.ToString()), "MaxAmount");
            requestContent.Add(new StringContent(request.RemainAmount.ToString()), "RemainAmount");
            requestContent.Add(new StringContent(request.IsgetLuckyNumbersRandom.ToString()), "IsgetLuckyNumbersRandom");
            requestContent.Add(new StringContent(request.NumberofLuckyNumbers.ToString()), "NumberofLuckyNumbers");
            requestContent.Add(new StringContent(request.LuckyNumberstr), "LuckyNumberstr");
            requestContent.Add(new StringContent(request.ExpiringDate.ToString()), "ExpiringDate");
            requestContent.Add(new StringContent(request.CreatedBy), "CreatedBy");
            requestContent.Add(new StringContent(request.CreatedTime.ToString()), "CreatedTime");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            var response = await _httpClient.PostAsync(uri, requestContent);

            var responsestring = await response.Content.ReadAsStringAsync();
            var apiresult = JsonConvert.DeserializeObject<APIResult<string>>(responsestring);
            return apiresult;
        }
        public async Task<Campaign?> DropCampaign(int id, string token)
        {
            var uri = BASE_REQUEST + $"/drop/{id}";
            //uri: ROOTPATH/campaign/drop/{id}
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetStringAsync(uri);
            var campaign = JsonConvert.DeserializeObject<Campaign>(response);
            return campaign;
        }
        public async Task<Campaign?> UnDropCampaign(int id, string token)
        {
            var uri = BASE_REQUEST + $"/undrop/{id}";
            //uri: ROOTPATH/campaign/drop/{id}
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetStringAsync(uri);
            var campaign = JsonConvert.DeserializeObject<Campaign>(response);
            return campaign;
        }
    }
}