﻿using eVoucher.ClientAPI_Integration;
using eVoucher_DTO.Models;
using eVoucher_Utility.Constants;
using eVoucher_Utility.Enums;
using eVoucher_ViewModel.Requests.CampaignRequests;
using eVoucher_ViewModel.Requests.VoucherRequests;
using eVoucher_ViewModel.Response;
using Microsoft.Extensions.Configuration;

namespace eVoucher_BUS.FrontendServices
{
    public interface IFrCampaignService
    {
        Task<PageResult<CampaignVM>> GetAllCampaignVMsPaging(string user, GetManageCampaignPagingRequest request, string token);

        Task<PageResult<VoucherTypeVM>> GetVoucherTypesOfCampaignPaging(int campaignid,
            GetManageCampaignPagingRequest request, string token);

        Task<CampaignVM?> GetCampaignVMById(int campaignid, string token);

        Task<List<Game>> GetAllGames(string token);

        Task<APIResult<string>> CreateCampaign(CampaignCreateRequest request, string token);

        Task<APIResult<string>> CreateVoucherType(CampaignCreateVoucherTypeRequest request, string token);
    }

    public class FrCampaignService : IFrCampaignService
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

        public async Task<CampaignVM?> GetCampaignVMById(int campaignid, string token)
        {
            var data = await _campaignAPIClient.GetAllCampaignVMsAsync(token);
            if (data == null)
            {
                return null;
            }
            var campaignvm = data.FirstOrDefault(c => c.Id == campaignid);
            string BaseAdress = _configuration[SystemConstants.AppSettings.BaseAddress];
            if (campaignvm != null)
            {
                campaignvm.ImagePath = BaseAdress + campaignvm.ImagePath;
                campaignvm.PartnerImagePath = BaseAdress + campaignvm.PartnerImagePath;
                foreach (var item in campaignvm.VoucherTypes)
                {
                    item.ImagePath = BaseAdress + item.ImagePath;
                }
            }
            return campaignvm;
        }

        //this function: GetAllCampaignVMsPaging (string user,...) for paging all active campaigns of a partner user
        //using for Partner app
        public async Task<PageResult<CampaignVM>> GetAllCampaignVMsPaging(string user, GetManageCampaignPagingRequest request,
            string token)
        {
            var data = await _campaignAPIClient.GetAllCampaignVMsAsync(token);
            if (string.IsNullOrEmpty(request.Keyword))
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

        public async Task<PageResult<VoucherTypeVM>> GetVoucherTypesOfCampaignPaging(int campaignid,
            GetManageCampaignPagingRequest request, string token)
        {
            var data = await _campaignAPIClient.GetAllCampaignVMsAsync(token);
            var filterdata = from vm in data
                             where (vm.Id == campaignid)
                             select vm;
            var campaigns = filterdata.ToList();
            var vouchertypes = new List<VoucherTypeVM>();
            string BaseAdress = _configuration[SystemConstants.AppSettings.BaseAddress];
            if (campaigns.Count > 0)
            {
                vouchertypes = campaigns[0].VoucherTypes;
                foreach (var item in vouchertypes)
                {
                    item.CampaignName = campaigns[0].Name;
                    item.ImagePath = BaseAdress + item.ImagePath;
                }
            }
            if (string.IsNullOrEmpty(request.Keyword)) { request.Keyword = ""; }
            var vouchertypesfilter = vouchertypes.Where(v => v.Name.ToLower().Contains(request.Keyword.ToLower()) ||
            v.Promotion.ToLower().Contains(request.Keyword.ToLower()));
            var pagedata = vouchertypesfilter.Skip((request.PageIndex - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .ToList();
            var pageresult = new PageResult<VoucherTypeVM>()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalItems = vouchertypes.Count(),
                Items = pagedata
            };
            return pageresult;
        }

        public async Task<APIResult<string>> CreateVoucherType(CampaignCreateVoucherTypeRequest request, string token)
        {
            return await _campaignAPIClient.CreateVoucherType(request, token);
        }
    }
}