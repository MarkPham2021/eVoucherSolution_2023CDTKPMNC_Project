using eVoucher_DAL.Repositories;
using eVoucher_DTO.Models;
using eVoucher_Utility.Enums;
using eVoucher_ViewModel.Requests.PartnerRequests;
using eVoucher_ViewModel.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_BUS.Services
{
    public interface IPartnerService
    {
        Task<List<Partner>> GetAllPartners();

        Task<PartnerVM?> GetPartnerById(int id);

        Task<APIResult<string>> RegisterPartner(PartnerCreateRequest request);

        Task<Partner?> UpdateStaff(PartnerEditRequest request);

        Task<Partner> DeletePartner(int id);

        Task<Partner> DeletePartner(Partner partner);
        Task<PartnerVM?> LockPartner(int id);
        Task<PartnerVM?> UnLockPartner(int id);
    }
    public class PartnerService : IPartnerService
    {
        private IPartnerRepository _partnerRepository;
        private IPartnerCategoryRepository _partnerCategoryRepository;
        private IFileStorageService _fileStorageService;
        private readonly UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        private const string USER_CONTENT_FOLDER_NAME = "eVoucher_images";
        public PartnerService(IPartnerRepository partnerRepository, IPartnerCategoryRepository partnerCategoryRepository,
            IFileStorageService fileStorageService, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _partnerRepository = partnerRepository;
            _partnerCategoryRepository = partnerCategoryRepository;
            _fileStorageService = fileStorageService;
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

        public async Task<List<Partner>> GetAllPartners()
        {
            return await _partnerRepository.GetAllAsync();
        }

        public async Task<PartnerVM?> GetPartnerById(int id)
        {
            var partner = await _partnerRepository.GetPartnerWithAppUserByCondition(p=>p.Id == id);
            return partner;
        }

        public async Task<APIResult<string>> RegisterPartner(PartnerCreateRequest request)
        {
            //add AppUser
            var user = new AppUser()
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserTypeId = request.UserTypeId
            };
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);
            var result = await _userManager.CreateAsync(user);
            //create partner
            var partner = new Partner()
            {
                Name = request.Name,
                Address = request.Address,
                Partnercategory = await _partnerCategoryRepository.GetSingleById(request.PartnerCategoryID),                
                AppUser = user,
                IsDeleted = false,
                Status = ActiveStatus.Active,
                CreatedBy = request.CreatedBy,
                CreatedTime = request.CreatedTime
            };
            //add image
            if (request.ImageFile != null)
            {
                partner.PartnerImages = new List<PartnerImage>()
                {
                    new PartnerImage()
                    {
                        Caption = $"{partner.Name}_Thumbnail_image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ImageFile.Length,
                        ImagePath = await this.SaveFile(request.ImageFile),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }
            var registerResult = await _partnerRepository.Add(partner);
            if (registerResult != null) { return new APIResult<string>(true, 
                "Registered account successfully", registerResult.Id.ToString()); }
            return new APIResult<string>(false, "Registered account fail", "Please try again");
        }

        public Task<Partner?> UpdateStaff(PartnerEditRequest request)
        {
            throw new NotImplementedException();
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _fileStorageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public async Task<PartnerVM?> LockPartner(int id)
        {
            return await _partnerRepository.LockPartner(id);
        }

        public async Task<PartnerVM?> UnLockPartner(int id)
        {
            return await _partnerRepository.UnLockPartner(id);
        }
    }
}
