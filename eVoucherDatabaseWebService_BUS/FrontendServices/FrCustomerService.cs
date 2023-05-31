using eVoucher.ClientAPI_Integration;
using eVoucher_DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_BUS.FrontendServices
{
    public interface IFrCustomerService
    {
        Task<List<Customer>?> GetAllCustomersFullInfo(string token);
        Task<Customer?> GetCustomerFullInfoById(int id, string token);
        Task<Customer?> GetCustomerFullInfoByUserInfo(string userinfo, string token);
    }
    public class FrCustomerService : IFrCustomerService
    {
        private readonly CustomerAPIClient _customerAPIClient;
        private readonly IFrCampaignService _campaignService;
        public FrCustomerService(CustomerAPIClient customerAPIClient, IFrCampaignService campaignService)
        {
            _customerAPIClient = customerAPIClient;
            _campaignService = campaignService;
        }

        public async Task<List<Customer>?> GetAllCustomersFullInfo(string token)
        {
            return await _customerAPIClient.GetAllCustomersFullInfo(token);
        }

        public async Task<Customer?> GetCustomerFullInfoById(int id, string token)
        {
            return await _customerAPIClient.GetSingleCustomerAllInfoById(id, token);
        }

        public async Task<Customer?> GetCustomerFullInfoByUserInfo(string userinfo, string token)
        {
            return await _customerAPIClient.GetSingleCustomerAllInfoByUserInfo(userinfo, token);
        }

    }
}
