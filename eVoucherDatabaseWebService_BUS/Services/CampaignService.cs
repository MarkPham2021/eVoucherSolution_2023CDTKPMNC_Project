using eVoucher_BUS.Requests.CampaignRequests;
using eVoucher_BUS.Response;
using eVoucher_DAL.Repositories;
using eVoucher_DTO.Models;
using eVoucher_Utility.Enums;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace eVoucher_BUS.Services
{
    public interface ICampaignService
    {
        List<Campaign> GetAllCampaigns();

        Task<Campaign?> GetCampaignById(int id);

        Task<APIResult<string>> CreateCampaign(CampaignCreateRequest request);

        Task<APIResult<string>> UpdateCampaign(CampaignEditRequest request);

        Task<APIResult<string>> DeletePartner(int id);
    }

    public class CampaignService : ICampaignService
    {
        private ICampaignRepository _campaignRepository;
        private IPartnerRepository _partnerRepository;
        private IFileStorageService _fileStorageService;
        private const string USER_CONTENT_FOLDER_NAME = "eVoucher_images";

        public CampaignService(ICampaignRepository campaignRepository, IPartnerRepository partnerRepository, IFileStorageService fileStorageService)
        {
            _campaignRepository = campaignRepository;
            _partnerRepository = partnerRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<APIResult<string>> CreateCampaign(CampaignCreateRequest request)
        {
            //declare partner and assign value for properties
            var campaign = new Campaign()
            {
                Name = request.Name,
                Slogan = request.Slogan,
                Partner = await _partnerRepository.GetSingleByCondition(x=>x.AppUser.Id==request.PartnerAppUserId),
                MetaKeyword = request.MetaKeyword,
                MetaDescription = request.MetaDescription,
                BeginningDate = request.BeginningDate,
                EndingDate = request.EndingDate,
                HomeFlag = request.HomeFlag,
                HotFlag = request.HotFlag,
                IsDeleted = false,
                Status = ActiveStatus.Active,
                CreatedBy = request.CreatedBy,
                CreatedTime = request.CreatedTime
            };
            //add image
            if (request.ImageFile != null)
            {
                campaign.CampaignImages = new List<CampaignImage>()
                {
                    new CampaignImage()
                    {
                        Caption = $"{campaign.Name}_Thumbnail_image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ImageFile.Length,
                        ImagePath = await this.SaveFile(request.ImageFile),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }
            var registerResult = await _campaignRepository.Add(campaign);
            if (registerResult != null)
            {
                return new APIResult<string>(true,
                $"Created campaign {campaign.Name} successfully", registerResult.Id.ToString());
            }
            return new APIResult<string>(false, $"Create campaign {campaign.Name} fail",
                "Please check data and try again");
        }

        public Task<APIResult<string>> DeletePartner(int id)
        {
            throw new NotImplementedException();
        }

        public List<Campaign> GetAllCampaigns()
        {
            throw new NotImplementedException();
        }

        public Task<Campaign?> GetCampaignById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<APIResult<string>> UpdateCampaign(CampaignEditRequest request)
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
    }
}