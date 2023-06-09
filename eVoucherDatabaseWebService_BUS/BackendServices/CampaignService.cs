﻿using eVoucher_DAL.Repositories;
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

        Task<APIResult<string>> EditCampaign(CampaignEditRequestforBackEnd request);

        Task<APIResult<string>> CreateCampaignVoucherType(CampaignCreateVoucherTypeRequest request);

        Task<APIResult<string>> DeleteCampaign(int id);

        Task<List<CampaignVM>> GetAllCampaignVMs();

        Task<Campaign?> DropCampaign(int id);

        Task<Campaign?> UnDropCampaign(int id);
    }

    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IPartnerRepository _partnerRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IVoucherTypeRepository _voucherTypeRepository;

        private readonly ICampaignGameRepository _campaignGameRepository;
        private readonly IFileStorageService _fileStorageService;
        private const string USER_CONTENT_FOLDER_NAME = "eVoucher_images";

        public CampaignService(ICampaignRepository campaignRepository, IPartnerRepository partnerRepository,
            IGameRepository gameRepository, ICampaignGameRepository campaignGameRepository,
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
            campaign.CampaignGames = new List<CampaignGame>();

            //process to assign campaigngame
            var selectitems = JsonConvert.DeserializeObject<List<SelectItem>>(request.Games);
            foreach (var item in selectitems)
            {
                if (item.IsSelected)
                {
                    var game = await _gameRepository.GetSingleById(item.Id);
                    var campaigngame = new CampaignGame()
                    {
                        Campaign = campaign,
                        Game = game,
                        Name = game.Name,
                        CreatedTime = DateTime.Now,
                        CreatedBy = request.CreatedBy,
                        IsDeleted = false,
                        Status = ActiveStatus.Active
                    };
                    campaign.CampaignGames.Add(campaigngame);
                    //var assigngameresult = await _campaignGameRepository.Add(campaigngame);
                    //add to game.campaignchosencount
                    game.CampaignChosenCount += 1;
                    var updategame = await _gameRepository.Update(game);
                }
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
            return apiresult;
        }

        public async Task<APIResult<string>> CreateCampaignVoucherType(CampaignCreateVoucherTypeRequest request)
        {
            APIResult<string> apiresult;
            if (string.IsNullOrEmpty(request.LuckyNumberstr))
            {
                request.LuckyNumberstr = "";
            }
            var vouchertype = new VoucherType()
            {
                Name = request.Name,
                Campaign = await _campaignRepository.GetSingleByCondition(x => x.Id == request.CampaignId),
                DiscountRate = request.DiscountRate,
                Promotion = request.Promotion,
                LuckyNumbers = request.LuckyNumberstr,
                MaxAmount = request.MaxAmount,
                RemainAmount = request.RemainAmount,
                ExpiringDate = request.ExpiringDate,
                IsDeleted = false,
                Status = ActiveStatus.Active,
                CreatedBy = request.CreatedBy,
                CreatedTime = request.CreatedTime
            };
            if (request.IsgetLuckyNumbersRandom)
            {
                var luckynumberlist = new List<int>();
                var rand = new Random();
                for (int i = 1; i <= request.NumberofLuckyNumbers; i++)
                {
                    int luckynumber = rand.Next(1, 1000);
                    while (luckynumberlist.Contains(luckynumber))
                    {
                        luckynumber = rand.Next(1, 1000);
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

        public async Task<List<CampaignVM>> GetAllCampaignVMs()
        {
            var result = await _campaignRepository.GetAllCampaignVMs();
            return result;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _fileStorageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public async Task<Campaign?> DropCampaign(int id)
        {
            return await _campaignRepository.DropCampaign(id);
        }

        public async Task<Campaign?> UnDropCampaign(int id)
        {
            return await _campaignRepository.UnDropCampaign(id);
        }

        public async Task<APIResult<string>> EditCampaign(CampaignEditRequestforBackEnd request)
        {
            var campaign = await _campaignRepository.GetSingleById(request.Id);
            campaign.Name = request.Name;
            campaign.Slogan = request.Slogan;
            campaign.MetaKeyword = request.MetaKeyword;
            campaign.MetaDescription = request.MetaDescription;
            campaign.HomeFlag = request.HomeFlag;
            campaign.HotFlag = request.HotFlag;
            campaign.BeginningDate = request.BeginningDate;
            campaign.EndingDate = request.EndingDate;
            campaign.UpdatedBy = request.UpdatedBy;
            campaign.UpdatedTime = request.UpdatedTime;
            //add image
            if (request.ImageFile != null)
            {
                if (campaign.CampaignImages == null)
                {
                    campaign.CampaignImages = new List<CampaignImage>();
                }
                if (campaign.CampaignImages.Count > 0)
                {
                    campaign.CampaignImages.Clear();
                }

                var image = new CampaignImage()
                {
                    Caption = $"{campaign.Name}_Thumbnail_image",
                    DateCreated = DateTime.Now,
                    FileSize = request.ImageFile.Length,
                    ImagePath = await this.SaveFile(request.ImageFile),
                    IsDefault = true,
                    SortOrder = 1
                };
                campaign.CampaignImages.Add(image);
            }
            if (campaign.CampaignGames == null)
            {
                campaign.CampaignGames = new List<CampaignGame>();
            }
            if (campaign.CampaignGames.Count > 0)
            {
                campaign.CampaignGames.Clear();
            }

            //process to assign campaigngame
            var selectitems = JsonConvert.DeserializeObject<List<SelectItem>>(request.Games);
            foreach (var item in selectitems)
            {
                if (item.IsSelected)
                {
                    var game = await _gameRepository.GetSingleById(item.Id);
                    var campaigngame = new CampaignGame()
                    {
                        Campaign = campaign,
                        Game = game,
                        Name = game.Name,
                        CreatedTime = DateTime.Now,
                        CreatedBy = request.UpdatedBy,
                        IsDeleted = false,
                        Status = ActiveStatus.Active
                    };
                    campaign.CampaignGames.Add(campaigngame);
                    game.CampaignChosenCount += 1;
                    var updategame = await _gameRepository.Update(game);
                }
            }
            var editResult = await _campaignRepository.Update(campaign);
            APIResult<string> apiresult;
            if (editResult != null)
            {
                apiresult = new APIResult<string>(true,
                $"Editted campaign {campaign.Name} successfully", editResult.Id.ToString());
            }
            else
            {
                apiresult = new APIResult<string>(false, $"Edit campaign {campaign.Name} fail",
                "Please check data and try again");
            }            
            return apiresult;
        }
    }
}