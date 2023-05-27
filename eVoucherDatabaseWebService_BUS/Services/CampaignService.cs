using eVoucher_DAL.Repositories;
using eVoucher_DTO.Models;
using eVoucher_Utility.Enums;
using eVoucher_ViewModel.Requests.CampaignRequests;
using eVoucher_ViewModel.Requests.Common;
using eVoucher_ViewModel.Response;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace eVoucher_BUS.Services
{
    public interface ICampaignService
    {
        Task<List<Campaign>> GetAllCampaigns();

        Task<Campaign?> GetCampaignById(int id);        

        Task<APIResult<string>> CreateCampaign(CampaignCreateRequestforBackend request);
        Task<APIResult<string>> CreateCampaignVoucherType(CampaignCreateVoucherTypeRequest request);
        Task<APIResult<string>> UpdateCampaign(CampaignEditRequest request);

        Task<APIResult<string>> DeleteCampaign(int id);
        Task<List<CampaignVM>> GetAllCampaignVMs();
    }

    public class CampaignService : ICampaignService
    {
        private ICampaignRepository _campaignRepository;
        private IPartnerRepository _partnerRepository;
        private IGameRepository _gameRepository;
        private IVoucherTypeRepository _voucherTypeRepository;
        
        private ICampaignGameRepository _campaignGameRepository;
        private IFileStorageService _fileStorageService;
        private const string USER_CONTENT_FOLDER_NAME = "eVoucher_images";

        public CampaignService(ICampaignRepository campaignRepository, IPartnerRepository partnerRepository,
            IGameRepository gameRepository,ICampaignGameRepository campaignGameRepository,
            IVoucherTypeRepository voucherTypeRepository, IFileStorageService fileStorageService)
        {
            _campaignRepository = campaignRepository;
            _partnerRepository = partnerRepository;
            _gameRepository = gameRepository;
            _campaignGameRepository = campaignGameRepository;
            _voucherTypeRepository = voucherTypeRepository;
            _fileStorageService = fileStorageService;
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
        public async Task<APIResult<string>> CreateCampaignVoucherType(CampaignCreateVoucherTypeRequest request)
        {
            APIResult<string> apiresult;
            var vouchertype = new VoucherType()
            {
                Name = request.Name,                
                Campaign = await _campaignRepository.GetSingleByCondition(x => x.Id == request.CampaignId),
                DiscountRate = request.DiscountRate,
                Promotion = request.Promotion,
                LuckyNumbers = request.LuckyNumberstr,
                MaxAmount = request.MaxAmount,
                RemainAmount = request.RemainAmount,
                IsDeleted = false,
                Status = ActiveStatus.Active,
                CreatedBy = request.CreatedBy,
                CreatedTime = request.CreatedTime
            };
            if (request.IsgetLuckyNumbersRandom)
            {
                var luckynumberlist = new List<int>();
                var rand =new Random();
                for (int i = 1; i <= request.NumberofLuckyNumbers; i++)
                {
                    int luckynumber = rand.Next(1, 100*request.NumberofLuckyNumbers);
                    while (luckynumberlist.Contains(luckynumber))
                    {
                        luckynumber = rand.Next(1, 100 * request.NumberofLuckyNumbers);
                    }
                    luckynumberlist.Add(luckynumber);
                }
                string luckynumbersstring = JsonConvert.SerializeObject(luckynumberlist);
                vouchertype.LuckyNumbers = luckynumbersstring;
            }
            //add image
            if (request.ImageFile != null)
            {
                vouchertype.VoucherTypeImages = new List<VoucherTypeImage>()
                {
                    new VoucherTypeImage()
                    {
                        Caption = $"{vouchertype.Name}_Thumbnail_image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ImageFile.Length,
                        ImagePath = await this.SaveFile(request.ImageFile),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }
            var registerResult = await _voucherTypeRepository.Add(vouchertype);
            
            if (registerResult != null)
            {
                apiresult = new APIResult<string>(true,
                $"Created vouchertype {vouchertype.Name} successfully", registerResult.Id.ToString());
            }
            else
            {
                apiresult = new APIResult<string>(false, $"Create campaign {vouchertype.Name} fail",
                "Please check data and try again");
            }            
            return apiresult;
        }
        public Task<APIResult<string>> DeleteCampaign(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Campaign>> GetAllCampaigns()
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
        public async Task<List<CampaignVM>> GetAllCampaignVMs() 
        {
            var includes = new string[] { "c => c.Partner", "c=>c.CampaignImages" };
            var data = await _campaignRepository.GetMulti(x=>x.Status==ActiveStatus.Active, includes);
            
            var result = new List<CampaignVM>();
            foreach (var vm in data)
            {
                CampaignVM item = new CampaignVM()
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    PartnerId = vm.Partner.Id,
                    PartnerName = vm.Partner.Name,
                    PartnerCategoryName = vm.Partner.Partnercategory.Name,
                    Slogan = vm.Slogan,
                    MetaKeyword = vm.MetaKeyword,
                    MetaDescription = vm.MetaDescription,
                    BeginningDate = vm.BeginningDate,
                    EndingDate = vm.EndingDate,
                    HomeFlag = vm.HomeFlag,
                    HotFlag = vm.HotFlag,
                    CreatedBy = vm.CreatedBy,
                    CreatedTime = vm.CreatedTime,
                    ImagePath = vm.CampaignImages[0].ImagePath                    
                };
                result.Add(item);
            }
            return result;
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