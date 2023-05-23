using eVoucher_BUS.Requests.PartnerRequests;
using eVoucher_DAL.Repositories;
using eVoucher_DTO.Models;
using eVoucher_Utility.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_BUS.Services
{
    public interface IPartnerService
    {
        List<Partner> GetAllPartners();

        Task<Partner?> GetPartnerById(int id);

        Task<Partner?> RegisterPartner(PartnerCreateRequest request);

        Task<Partner?> UpdateStaff(PartnerEditRequest request);

        Task<Partner> DeletePartner(int id);

        Task<Partner> DeletePartner(Partner partner);
    }
    public class PartnerService : IPartnerService
    {
        private IPartnerRepository _partnerRepository;
        private readonly UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        public PartnerService(IPartnerRepository partnerRepository, UserManager<AppUser> userManager, 
            RoleManager<AppRole> roleManager)
        {
            _partnerRepository = partnerRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task<Partner> DeletePartner(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Partner> DeletePartner(Partner partner)
        {
            throw new NotImplementedException();
        }

        public List<Partner> GetAllPartners()
        {
            throw new NotImplementedException();
        }

        public Task<Partner?> GetPartnerById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Partner?> RegisterPartner(PartnerCreateRequest request)
        {
            var user = new AppUser()
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserTypeId = request.UserTypeId
            };
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);
            var result = await _userManager.CreateAsync(user);
            var staff = new Partner()
            {
                Name = request.Name,
                //Partnercategory = request.PartnerCategoryID,
                //Department = request.Department,
                //CreatedBy = request.CreatedBy,
                //CreatedTime = request.CreatedTime,
                //IsDeleted = false,
                //Status = ActiveStatus.Active,
                //AppUser = user
            };
            var registerResult = await _partnerRepository.Add(staff);

            return registerResult;
        }

        public Task<Partner?> UpdateStaff(PartnerEditRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
