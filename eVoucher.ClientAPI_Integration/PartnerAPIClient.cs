using Azure.Core;
using eVoucher_BUS.Requests.PartnerRequests;
using eVoucher_BUS.Requests.StaffRequests;
using eVoucher_BUS.Response;
using eVoucher_DTO.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher.ClientAPI_Integration
{
    public class PartnerAPIClient : BaseAPIClient
    {
        const string BASE_REQUEST = "partner";
        public PartnerAPIClient():base() { }
        public async Task<List<PartnerCategory>?> GetAllPartnerCategoriesAsync()
        {
            var uri = BASE_REQUEST + "/getallpartnercategories";
            //uri: ROOTPATH/partner/getallpartnercategories
            var response = await _httpClient.GetStringAsync(uri);            
            var categories = JsonConvert.DeserializeObject<List<PartnerCategory>>(response);
            return categories;
        }
        public async Task<List<Partner>?> GetAllPartnersAsync()
        {
            var uri = BASE_REQUEST ;
            //uri: ROOTPATH/partner
            var response = await _httpClient.GetStringAsync(uri);
            var partners = JsonConvert.DeserializeObject<List<Partner>>(response);
            return partners;
        }
        public async Task<APIResult<string>> Register(PartnerCreateRequest request)
        {
            var uri = BASE_REQUEST +"/register";
            
            var requestContent = new MultipartFormDataContent();

            if (request.ImageFile != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ImageFile.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ImageFile.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ImageFile",$"{request.Name}thumbnailImage{request.ImageFile.FileName}");
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

    }
}
