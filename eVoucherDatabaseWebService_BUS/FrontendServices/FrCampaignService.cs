using Azure.Core;
using eVoucher.ClientAPI_Integration;
using eVoucher_DTO.Models;
using eVoucher_Utility.Constants;
using eVoucher_Utility.Enums;
using eVoucher_ViewModel.Requests.CampaignRequests;
using eVoucher_ViewModel.Response;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace eVoucher_BUS.FrontendServices
{
    public interface IFrCampaignService
    {
        Task<PageResult<CampaignVM>> GetAllCampaignVMsPaging(string user, GetManageCampaignPagingRequest request,string token);
        Task<List<Game>> GetAllGames(string token);
        Task<APIResult<string>> CreateCampaign(CampaignCreateRequest request,string token);

    }

    public class FrCampaignService: IFrCampaignService
    {
        private CampaignAPIClient _campaignAPIClient;
        private GameAPIClient _gameAPIClient;
        private IConfiguration _configuration;
        public FrCampaignService(CampaignAPIClient campaignAPIClient, GameAPIClient gameAPIClient,
            IConfiguration configuration)
        {
            _campaignAPIClient = campaignAPIClient;
            _gameAPIClient = gameAPIClient;
            _configuration = configuration;
        }

        public async Task<APIResult<string>> CreateCampaign(CampaignCreateRequest request, string token)
        {
            return await _campaignAPIClient.Create(request, token); 
        }

        public async Task<List<Game>> GetAllGames(string token)
        {
            return await _gameAPIClient.GetAllGameAsync(token);
        }

        public async Task<PageResult<CampaignVM>> GetAllCampaignVMsPaging(string user, GetManageCampaignPagingRequest request, 
            string token)
        {
            var data = await _campaignAPIClient.GetAllCampaignVMsAsync(token);
            if(string.IsNullOrEmpty(request.Keyword))
            {
                request.Keyword = "";
            }
            var filterdata = from vm in data 
                             where (vm.Name.ToLower().Contains(request.Keyword.ToLower()) || 
                             vm.MetaKeyword.ToLower().Contains(request.Keyword.ToLower()) ||
                             vm.MetaDescription.ToLower().Contains(request.Keyword.ToLower())) && (vm.CreatedBy.ToLower() == user.ToLower()) &&
                             (vm.Status == ActiveStatus.Active)
                             select vm;
            var pagedata = filterdata.Skip((request.PageIndex - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .ToList();
            string BaseAdress = _configuration[SystemConstants.AppSettings.BaseAddress];
            foreach (var item in pagedata)
            {
                item.ImagePath = BaseAdress + item.ImagePath;
            }
            var pageresult = new PageResult<CampaignVM>()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalItems = filterdata.Count(),
                Items = pagedata
            };
            return pageresult;
        }
    }
}