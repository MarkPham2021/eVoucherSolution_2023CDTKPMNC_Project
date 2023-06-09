﻿using eVoucher_DTO.Models;
using eVoucher_ViewModel.Requests.PartnerRequests;
using eVoucher_ViewModel.Response;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace eVoucher.ClientAPI_Integration
{
    public class PartnerAPIClient : BaseAPIClient
    {
        private const string BASE_REQUEST = "partner";
        private readonly IConfiguration _configuration;

        public PartnerAPIClient(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<PartnerCategory>?> GetAllPartnerCategoriesAsync(string token)
        {
            //uri: ROOTPATH/partner/getallpartnercategories
            var uri = BASE_REQUEST + "/getallpartnercategories";            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetStringAsync(uri);
            var categories = JsonConvert.DeserializeObject<List<PartnerCategory>>(response);
            return categories;
        }

        public async Task<List<Partner>?> GetAllPartnersAsync(string token)
        {
            var uri = BASE_REQUEST;
            //uri: ROOTPATH/partner
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetStringAsync(uri);
            var partners = JsonConvert.DeserializeObject<List<Partner>>(response);
            return partners;
        }

        public async Task<APIResult<string>> Register(PartnerCreateRequest request)
        {
            var uri = BASE_REQUEST + "/register";

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
            requestContent.Add(new StringContent(request.Address), "Address");
            requestContent.Add(new StringContent(request.PartnerCategoryID.ToString()), "PartnerCategoryID");
            requestContent.Add(new StringContent(request.Email), "Email");
            requestContent.Add(new StringContent(request.PhoneNumber), "PhoneNumber");
            requestContent.Add(new StringContent(request.UserName), "UserName");
            requestContent.Add(new StringContent(request.UserTypeId.ToString()), "UserTypeId");
            requestContent.Add(new StringContent(request.Password), "Password");
            requestContent.Add(new StringContent(request.ConfirmPassword), "ConfirmPassword");
            requestContent.Add(new StringContent(request.CreatedBy), "CreatedBy");
            requestContent.Add(new StringContent(request.CreatedTime.ToString()), "CreatedTime");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            var response = await _httpClient.PostAsync(uri, requestContent);

            var responsestring = await response.Content.ReadAsStringAsync();
            var apiresult = JsonConvert.DeserializeObject<APIResult<string>>(responsestring);
            return apiresult;
        }
        public async Task<PartnerVM> LockPartner(int id, string token)
        {
            var uri = BASE_REQUEST +$"/lock/{id}";
            //uri: ROOTPATH/partner/lock/{id}
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetStringAsync(uri);
            var partnervm = JsonConvert.DeserializeObject<PartnerVM>(response);
            return partnervm;
        }
        public async Task<PartnerVM> UnLockPartner(int id, string token)
        {
            var uri = BASE_REQUEST + $"/unlock/{id}";
            //uri: ROOTPATH/partner/unlock/{id}
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetStringAsync(uri);
            var partnervm = JsonConvert.DeserializeObject<PartnerVM>(response);
            return partnervm;
        }
    }
}