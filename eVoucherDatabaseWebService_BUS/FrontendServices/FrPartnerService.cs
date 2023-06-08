using eVoucher.ClientAPI_Integration;
using eVoucher_DTO.Models;
using eVoucher_Utility.Constants;
using eVoucher_Utility.Enums;
using eVoucher_ViewModel.Requests.PartnerRequests;
using eVoucher_ViewModel.Requests.UserRequests;
using eVoucher_ViewModel.Response;
using Microsoft.Extensions.Configuration;

namespace eVoucher_BUS.FrontendServices
{
    public interface IFrPartnerService
    {
        public Task<APIResult<string>> Login(LoginRequest request);

        public Task<List<PartnerCategory>?> GetPartnerCategoriesAsync(string token);

        public Task<APIResult<string>> Register(PartnerCreateRequest request);

        public Task<Partner?> GetPartnerByIdAsync(int id, string token);

        public Task<Partner?> GetPartnerByUserInfoAsync(string userinfo, string token);

        Task<PageResult<Partner>> GetAllPartnerPaging(GetAdminPartnersPagingRequest request, string token);
        Task<PartnerVM> LockPartner (int id, string token);
        Task<PartnerVM> UnLockPartner(int id, string token);
    }

    public class FrPartnerService : IFrPartnerService
    {
        private PartnerAPIClient _partnerapiclient;
        private LoginAPIClient _loginapiclient;
        private ICommonService _commonservice;
        private IConfiguration _configuration;

        public FrPartnerService(PartnerAPIClient partnerapiclient, LoginAPIClient loginapiclient,
            ICommonService commonservice, IConfiguration configuration)
        {
            _partnerapiclient = partnerapiclient;
            _loginapiclient = loginapiclient;
            _commonservice = commonservice;
            _configuration = configuration;
        }
        //function Task<PageResult<Partner>> GetAllPartnerPaging use for paging partner on admin partner_indexView
        public async Task<PageResult<Partner>> GetAllPartnerPaging(GetAdminPartnersPagingRequest request, string token)
        {
            var partners = await _partnerapiclient.GetAllPartnersAsync(token);
            var filterdata = from vm in partners
                             where (vm.Name.ToLower().Contains(request.Keyword.ToLower()) ||
                             vm.AppUser.Email.ToLower().Contains(request.Keyword.ToLower()) ||
                             vm.AppUser.PhoneNumber.Contains(request.Keyword))
                             select vm;
            if (request.CategoryId > 0)
            {
                filterdata = from vm in filterdata
                             where vm.Partnercategory.Id == request.CategoryId
                             select vm;
            }
            var pagedata = filterdata.Skip((request.PageIndex - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .ToList();
            string BaseAdress = _configuration[SystemConstants.AppSettings.BaseAddress];
            foreach (var item in pagedata)
            {
                if (item.PartnerImages.Count > 0)
                {
                    item.PartnerImages[0].ImagePath = BaseAdress + item.PartnerImages[0].ImagePath;
                }
            }
            var pageresult = new PageResult<Partner>()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalItems = filterdata.Count(),
                Items = pagedata
            };
            return pageresult;
        }

        public Task<Partner?> GetPartnerByIdAsync(int id, string token)
        {
            throw new NotImplementedException();
        }

        public Task<Partner?> GetPartnerByUserInfoAsync(string userinfo, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PartnerCategory>?> GetPartnerCategoriesAsync(string token)
        {
            return await _partnerapiclient.GetAllPartnerCategoriesAsync(token);
        }

        public async Task<PartnerVM> LockPartner(int id, string token)
        {
            var partnervm = await _partnerapiclient.LockPartner(id, token);
            if(partnervm != null)
            {
                if (partnervm.Partner.PartnerImages.Count > 0)
                {
                    partnervm.Partner.PartnerImages[0].ImagePath = _configuration[SystemConstants.AppSettings.BaseAddress] +
                                                                    partnervm.Partner.PartnerImages[0].ImagePath;
                }
            }
            return partnervm;
        }

        public async Task<APIResult<string>> Login(LoginRequest request)
        {
            return await _loginapiclient.Login(request);
        }

        public async Task<APIResult<string>> Register(PartnerCreateRequest request)
        {
            return await _partnerapiclient.Register(request);
        }

        public async Task<PartnerVM> UnLockPartner(int id, string token)
        {
            var partnervm = await _partnerapiclient.UnLockPartner(id, token);
            if (partnervm != null)
            {
                if (partnervm.Partner.PartnerImages.Count > 0)
                {
                    partnervm.Partner.PartnerImages[0].ImagePath = _configuration[SystemConstants.AppSettings.BaseAddress] +
                                                                    partnervm.Partner.PartnerImages[0].ImagePath;
                }
            }
            return partnervm;
        }
    }
}