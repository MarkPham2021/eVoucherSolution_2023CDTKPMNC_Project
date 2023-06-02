using eVoucher.ClientAPI_Integration;
using eVoucher_DTO.Models;
using eVoucher_Utility.Constants;
using eVoucher_Utility.Enums;
using eVoucher_ViewModel.Requests.CampaignRequests;
using eVoucher_ViewModel.Requests.Common;
using eVoucher_ViewModel.Requests.VoucherRequests;
using eVoucher_ViewModel.Response;
using Microsoft.Extensions.Configuration;

namespace eVoucher_BUS.FrontendServices
{
    public interface IFrCustomerService
    {
        Task<List<Customer>?> GetAllCustomersFullInfo(string token);

        Task<Customer?> GetCustomerFullInfoById(int id, string token);

        Task<Customer?> GetCustomerFullInfoByUserInfo(string userinfo, string token);

        Task<List<VoucherVM>?> GetAllVouchersOfCustomerByUserInfo(string userinfo, string token);

        Task<List<VoucherVM>?> GetAllVouchersOfCustomerByCustomerId(int id, string token);

        Task<VoucherVM?> GetVoucherVMById(int id, string token);

        Task<PageResult<VoucherVM>> GetCustomerVouchersPaging(string user, GetCustomerVouchersRequestPaging request,
            string token);
        Task<PageResult<CampaignVM>> GetCustomerCampaignVMsPaging(GetCustomerCampaignPagingRequest request,
            string token);
    }

    public class FrCustomerService : IFrCustomerService
    {
        private readonly CustomerAPIClient _customerAPIClient;
        private readonly IFrCampaignService _campaignService;
        private readonly CampaignAPIClient _campaignAPIClient;
        private readonly GoogleDistanceMatrixAPICLient _googleDistanceMatrixAPI;
        private IConfiguration _configuration;

        public FrCustomerService(CustomerAPIClient customerAPIClient, IFrCampaignService campaignService,
            CampaignAPIClient campaignAPIClient, IConfiguration configuration, 
            GoogleDistanceMatrixAPICLient googleDistanceMatrixAPICLient)
        {
            _customerAPIClient = customerAPIClient;
            _campaignService = campaignService;
            _campaignAPIClient = campaignAPIClient;
            _googleDistanceMatrixAPI = googleDistanceMatrixAPICLient;
            _configuration = configuration;
        }

        public async Task<List<Customer>?> GetAllCustomersFullInfo(string token)
        {
            return await _customerAPIClient.GetAllCustomersFullInfo(token);
        }

        public async Task<List<VoucherVM>?> GetAllVouchersOfCustomerByCustomerId(int id, string token)
        {
            return await _customerAPIClient.GetAllVouchersOfCustomerByCustomerId(id, token);
        }

        public async Task<List<VoucherVM>?> GetAllVouchersOfCustomerByUserInfo(string userinfo, string token)
        {
            return await _customerAPIClient.GetAllVouchersOfCustomerByUserInfo(userinfo, token);
        }

        public async Task<Customer?> GetCustomerFullInfoById(int id, string token)
        {
            return await _customerAPIClient.GetSingleCustomerAllInfoById(id, token);
        }

        public async Task<Customer?> GetCustomerFullInfoByUserInfo(string userinfo, string token)
        {
            return await _customerAPIClient.GetSingleCustomerAllInfoByUserInfo(userinfo, token);
        }

        public async Task<VoucherVM?> GetVoucherVMById(int id, string token)
        {
            return await _customerAPIClient.GetVoucherVMById(id, token);
        }

        public async Task<PageResult<VoucherVM>> GetCustomerVouchersPaging(string user, GetCustomerVouchersRequestPaging request,
            string token)
        {
            var data = await GetAllVouchersOfCustomerByUserInfo(user, token);
            if (string.IsNullOrEmpty(request.Keyword))
            {
                request.Keyword = "";
            }
            var filterdata = from vm in data
                             where (vm.CampaignName.ToLower().Contains(request.Keyword.ToLower()) ||
                             vm.PartnerCategoryName.ToLower().Contains(request.Keyword.ToLower()) ||
                             vm.PartnerName.ToLower().Contains(request.Keyword.ToLower()))
                             select vm;
            var pagedata = filterdata.Skip((request.PageIndex - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .ToList();
            string BaseAdress = _configuration[SystemConstants.AppSettings.BaseAddress];
            foreach (var item in pagedata)
            {
                item.VoucherTypeImagePath = BaseAdress + item.VoucherTypeImagePath;
            }
            var pageresult = new PageResult<VoucherVM>()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalItems = filterdata.Count(),
                Items = pagedata
            };
            return pageresult;
        }
        public async Task<PageResult<CampaignVM>> GetCustomerCampaignVMsPaging(GetCustomerCampaignPagingRequest request,
            string token)
        {
            var data = await _campaignAPIClient.GetAllCampaignVMsAsync(token);            
            //filter by keyword
            var filterdata = from vm in data
                             where (vm.Name.ToLower().Contains(request.keyword.ToLower()) ||
                             vm.MetaKeyword.ToLower().Contains(request.keyword.ToLower()) ||
                             vm.MetaDescription.ToLower().Contains(request.keyword.ToLower())) &&
                             (vm.Status == ActiveStatus.Active)
                             select vm;
            
            //filter by category
            if (request.categoryId > 0)
            {
                var filterbycat = from vm in filterdata
                                  where vm.PartnerCategoryId == request.categoryId
                                  select vm;
                foreach(var c in filterbycat)
                {
                    var getdistanceRequest = new GetGoogleDistanceMatrixRequest()
                    {
                        destinations = c.PartnerAddress,
                        origins = request.currentAddress,
                        key = ""
                    };
                    var d = await _googleDistanceMatrixAPI.GetDistanceMatrix(getdistanceRequest);
                    c.DistanceToCustomer = d.value;
                    c.DistanceToCustomerInChar = d.text;
                }   
                //filter by filter : nearby/latest
                if(request.filter == 2)//filter latest
                {
                    var Pagingdata = filterbycat.OrderByDescending(x => x.CreatedTime);
                    var pagedata = Pagingdata.Skip((request.PageIndex - 1) * request.PageSize)
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
                }else //if(request.filter == 1)filter nearby
                {
                    var Pagingdata = filterbycat.OrderBy(x => x.DistanceToCustomer);
                    var pagedata = Pagingdata.Skip((request.PageIndex - 1) * request.PageSize)
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
            else
            {
                foreach (var c in filterdata)
                {
                    var getdistanceRequest = new GetGoogleDistanceMatrixRequest()
                    {
                        destinations = c.PartnerAddress,
                        origins = request.currentAddress,
                        key = ""
                    };
                    var d = await _googleDistanceMatrixAPI.GetDistanceMatrix(getdistanceRequest);
                    c.DistanceToCustomer = d.value;
                    c.DistanceToCustomerInChar = d.text;
                }
                //filter by filter : nearby/latest
                if (request.filter == 2)//filter latest
                {
                    var Pagingdata = filterdata.OrderByDescending(x => x.CreatedTime);
                    var pagedata = Pagingdata.Skip((request.PageIndex - 1) * request.PageSize)
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
                else //if (request.filter == 1) filter nearby
                {
                    var Pagingdata = filterdata.OrderBy(x => x.DistanceToCustomer);
                    var pagedata = Pagingdata.Skip((request.PageIndex - 1) * request.PageSize)
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
    }
}