using eVoucher.ClientAPI_Integration;
using eVoucher_DTO.Models;
using eVoucher_ViewModel.Requests.PartnerRequests;
using eVoucher_ViewModel.Requests.UserRequests;
using eVoucher_ViewModel.Response;
using Microsoft.Extensions.Configuration;

namespace eVoucher_BUS.FrontendServices
{
    public interface IFrPartnerService
    {
        public Task<APIResult<string>> Login(LoginRequest request);

        public Task<List<PartnerCategory>?> GetPartnerCategoriesAsync();

        public Task<APIResult<string>> Register(PartnerCreateRequest request);

        public Task<Partner?> GetPartnerByIdAsync(int id);
        public Task<Partner?> GetPartnerByUserInfoAsync(string userinfo);
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

        public Task<Partner?> GetPartnerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Partner?> GetPartnerByUserInfoAsync(string userinfo)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PartnerCategory>?> GetPartnerCategoriesAsync()
        {
            return await _partnerapiclient.GetAllPartnerCategoriesAsync();
        }

        public async Task<APIResult<string>> Login(LoginRequest request)
        {
            return await _loginapiclient.Login(request);
        }

        public async Task<APIResult<string>> Register(PartnerCreateRequest request)
        {
            return await _partnerapiclient.Register(request);
        }
    }
}