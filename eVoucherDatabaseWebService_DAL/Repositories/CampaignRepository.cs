using eVoucher_DAL.InfraStructure;
using eVoucher_ViewModel.Requests.CampaignRequests;
using eVoucher_ViewModel.Requests.GameRequests;
using eVoucher_ViewModel.Requests.VoucherRequests;

namespace eVoucher_DAL.Repositories
{
    public interface ICampaignRepository : IRepository<Campaign>
    {
        Task<List<CampaignVM>> GetAllCampaignVMs();
    }

    public class CampaignRepository : RepositoryBase<Campaign>, ICampaignRepository
    {


        public CampaignRepository(eVoucherDbContext context) : base(context) { }

        public async Task<List<CampaignVM>> GetAllCampaignVMs()
        {
            var data = _context.Campaigns
                .Include(c => c.Partner)
                .ThenInclude(Partner => Partner.Partnercategory)                
                .Include(c => c.CampaignGames)
                .ThenInclude(CampaignGame => CampaignGame.Game)
                .Include(c => c.CampaignImages)
                .Include(c => c.VoucherTypes)
                .ThenInclude(VoucherType=> VoucherType.VoucherTypeImages)
                .ToList();
            var campaignvmlist = new List<CampaignVM>();
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
                    ImagePath = vm.CampaignImages[0].ImagePath,
                    campaignGames = new List<CampaignGameVM>(),
                    VoucherTypes = new List<VoucherTypeVM>()
                };
                //get list of CampaignGames of campaign                
                foreach (var g in vm.CampaignGames)
                {
                    var gvm = new CampaignGameVM()
                    {
                        Id = g.Id,
                        Name = g.Name
                    };
                    item.campaignGames.Add(gvm);
                }
                //get list of vouchertypes of the campaign
                foreach(var v in vm.VoucherTypes)
                {
                    var vvm = new VoucherTypeVM() 
                    { 
                        Id = v.Id,
                        Name = v.Name,
                        CampaignId = item.Id,
                        DiscountRate = v.DiscountRate,
                        Promotion = v.Promotion,
                        LuckyNumbers = v.LuckyNumbers,

                        ExpiringDate = v.ExpiringDate,
                        MaxAmount = v.MaxAmount,
                        RemainAmount = v.RemainAmount,
                        ImagePath = v.VoucherTypeImages[0].ImagePath

                    };
                    item.VoucherTypes.Add(vvm);
                }
                campaignvmlist.Add(item);
            }
            return campaignvmlist;
        }
    }
}