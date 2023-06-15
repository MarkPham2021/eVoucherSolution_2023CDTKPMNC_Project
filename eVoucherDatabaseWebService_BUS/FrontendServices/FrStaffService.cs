using eVoucher.ClientAPI_Integration;
using eVoucher_DTO.Models;
using eVoucher_Utility.Constants;
using eVoucher_ViewModel.Requests.PartnerRequests;
using eVoucher_ViewModel.Requests.StaffRequests;
using eVoucher_ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_BUS.FrontendServices
{
    public interface IFrStaffService
    {
        Task<Staff> Register(StaffRegisterRequest request);
        Task<PageResult<Staff>> GetAllStaffPaging(GetAllStaffPagingRequest request, string token);
        Task<Staff> GetSingleById(int id, string token);
        Task<Staff> Activate(int id, string token);
        Task<Staff> Lock(int id, string token);

    }
    public class FrStaffService : IFrStaffService
    {
        private readonly StaffAPIClient _staffAPIClient;
        public FrStaffService(StaffAPIClient staffAPIClient)
        {
            _staffAPIClient = staffAPIClient;
        }
        public async Task<Staff> Activate(int id, string token)
        {
            return await _staffAPIClient.Activate(id,token);
        }

        public async Task<PageResult<Staff>> GetAllStaffPaging(GetAllStaffPagingRequest request, string token)
        {
            var data =  await _staffAPIClient.GetAll(token);
            var filterdata = from vm in data
                             where (vm.Name.ToLower().Contains(request.Keyword.ToLower()) ||
                             vm.AppUser.Email.ToLower().Contains(request.Keyword.ToLower()) ||
                             vm.AppUser.PhoneNumber.Contains(request.Keyword))
                             select vm;
            
            filterdata = filterdata.OrderByDescending(x => x.CreatedTime);
            var pagedata = filterdata.Skip((request.PageIndex - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .ToList();            
            var pageresult = new PageResult<Staff>()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalItems = filterdata.Count(),
                Items = pagedata
            };
            return pageresult;
        }

        public async Task<Staff> GetSingleById(int id, string token)
        {
            return await _staffAPIClient.GetSingleById(id,token);
        }

        public async Task<Staff> Lock(int id, string token)
        {
            return await (_staffAPIClient.Lock(id,token));
        }

        public async Task<Staff> Register(StaffRegisterRequest request)
        {
            return await _staffAPIClient.Register(request);
        }
    }
}
