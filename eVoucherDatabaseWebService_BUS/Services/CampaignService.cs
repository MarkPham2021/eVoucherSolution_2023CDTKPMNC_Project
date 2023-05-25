using eVoucher_BUS.Requests.CampaignRequests;
using eVoucher_BUS.Requests.Common;
using eVoucher_BUS.Response;
using eVoucher_DAL.Repositories;
using eVoucher_DTO.Models;
using eVoucher_Utility.Enums;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace eVoucher_BUS.Services
{
    public interface ICampaignService
    {
        List<Campaign> GetAllCampaigns();

        Task<Campaign?> GetCampaignById(int id);

        Task<APIResult<string>> CreateCampaignNotAssignGames(CampaignCreateRequest request);

        Task<APIResult<string>> CreateCampaign(CampaignCreateRequestforBackend request);
        Task<APIResult<string>> UpdateCampaign(CampaignEditRequest request);

        Task<APIResult<string>> DeletePartner(int id);
    }

    public class CampaignService : ICampaignService
    {
        private ICampaignRepository _campaignRepository;
        private IPartnerRepository _partnerRepository;
        private IGameRepository _gameRepository;
        private ICampaignGameRepository _campaignGameRepository;
        private IFileStorageService _fileStorageService;
        private const string USER_CONTENT_FOLDER_NAME = "eVoucher_images";

        public CampaignService(ICampaignRepository campaignRepository, IPartnerRepository partnerRepository,
            IGameRepository gameRepository,ICampaignGameRepository campaignGameRepository,
            IFileStorageService fileStorageService)
        {
            _campaignRepository = campaignRepository;
            _partnerRepository = partnerRepository;
            _gameRepository = gameRepository;
            _campaignGameRepository = campaignGameRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<APIResult<string>> CreateCampaignNotAssignGames(CampaignCreateRequest request)
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
        public async Task<APIResult<string>> CreateCampaign(CampaignCreateRequestforBackend request)
        {
            //declare campaign and assign value for properties

            var campaign = new Campaign()
            {
                Name = request.Name,
                Slogan = request.Slogan,
                Partner = await _partnerRepository.GetSingleByCondition(x => x.AppUser.Id == request.PartnerAppUserId),
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
            APIResult<string> apiresult;
            if (registerResult != null)
            {
                apiresult = new APIResult<string>(true,
                $"Created campaign {campaign.Name} successfully", registerResult.Id.ToString());
            }
            else
            {
                apiresult = new APIResult<string>(false, $"Create campaign {campaign.Name} fail",
                "Please check data and try again");
            }             
            //process to assign campaigngame
            var selectitems = JsonConvert.DeserializeObject<List<SelectItem>>(request.Games);            
            foreach(var item in selectitems)
            {
                if(item.IsSelected)
                {
                    var game = await _gameRepository.GetSingleById(item.Id);
                    var campaigngame = new CampaignGame()
                    {
                        Campaign = campaign,
                        Game = game,
                        Name = game.Name + campaign.Name,
                        CreatedTime = DateTime.Now,
                        CreatedBy = request.CreatedBy,
                        IsDeleted = false,
                        Status = ActiveStatus.Active
                    };
                    var assigngameresult = await _campaignGameRepository.Add(campaigngame);
                }
            }            
            return apiresult;
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