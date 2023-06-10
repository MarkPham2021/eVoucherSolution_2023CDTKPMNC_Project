using eVoucher_DAL.InfraStructure;
using eVoucher_ViewModel.Requests.CampaignRequests;
using eVoucher_ViewModel.Requests.GameRequests;
using eVoucher_ViewModel.Requests.VoucherRequests;

namespace eVoucher_DAL.Repositories
{
    public interface ICampaignRepository : IRepository<Campaign>
    {
        Task<List<CampaignVM>> GetAllCampaignVMs();
        Task<Campaign?> DropCampaign(int id);
        Task<Campaign?> UnDropCampaign(int id);
    }

    public class CampaignRepository : RepositoryBase<Campaign>, ICampaignRepository
    {


        public CampaignRepository(eVoucherDbContext context) : base(context) { }

        public async Task<Campaign?> DropCampaign(int id)
        {
            var dropCampaign = await _context.Database.ExecuteSqlAsync($"UPDATE [Campaigns] SET [Status] = 0, [UpdatedTime] ={DateTime.Now} WHERE [Id] = {id}");
            var campaigns = await _context.Set<Campaign>()
                .Include(c => c.Partner)
                .ThenInclude(p => p.Partnercategory)
                .Include(c => c.CampaignImages)
                .Where(c => c.Id == id).ToListAsync();
            if (!campaigns.Any())
            {
                return null;
            }
            return campaigns[0];
        }

        public async Task<List<CampaignVM>> GetAllCampaignVMs()
        {
            var data = await _context.Campaigns
                .Include(c=>c.Partner)
                .ThenInclude(Partner => Partner.AppUser)
                .Include(c => c.Partner)
                .ThenInclude(Partner => Partner.Partnercategory)
                .Include(c => c.Partner)
                .ThenInclude(Partner => Partner.PartnerImages)
                .Include(c => c.CampaignGames)
                .ThenInclude(CampaignGame => CampaignGame.Game)
                .Include(c => c.CampaignImages)
                .Include(c => c.VoucherTypes)
                .ThenInclude(VoucherType=> VoucherType.VoucherTypeImages)
                .ToListAsync();
            var campaignvmlist = new List<CampaignVM>();
            foreach (var vm in data)
            {
                CampaignVM item = new CampaignVM()
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    PartnerId = vm.Partner.Id,
                    PartnerName = vm.Partner.Name,
                    PartnerCategoryId = vm.Partner.Partnercategory.Id,
                    PartnerCategoryName = vm.Partner.Partnercategory.Name,
                    PartnerPhoneNumber = vm.Partner.AppUser.PhoneNumber,
                    PartnerAddress = vm.Partner.Address,
                    Slogan = vm.Slogan,
                    MetaKeyword = vm.MetaKeyword,
                    MetaDescription = vm.MetaDescription,
                    BeginningDate = vm.BeginningDate,
                    EndingDate = vm.EndingDate,
                    HomeFlag = vm.HomeFlag,
                    HotFlag = vm.HotFlag,
                    CreatedBy = vm.CreatedBy,
                    CreatedTime = vm.CreatedTime,
                    Status = vm.Status,
                    IsDeleted = vm.IsDeleted,
                    ImagePath = vm.CampaignImages[0].ImagePath,
                    PartnerImagePath = vm.Partner.PartnerImages[0].ImagePath,
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

        public async Task<Campaign?> UnDropCampaign(int id)
        {
            var undropCampaign = await _context.Database.ExecuteSqlAsync($"UPDATE [Campaigns] SET [Status] = 1,[UpdatedTime] ={DateTime.Now} WHERE [Id] = {id}");
            var campaigns = await _context.Set<Campaign>()
                .Include(c => c.Partner)
                .ThenInclude(p => p.Partnercategory)
                .Include(c => c.CampaignImages)
                .Where(c => c.Id == id).ToListAsync();
            if (!campaigns.Any())
            {
                return null;
            }
            return campaigns[0];
        }
        public override async Task<Campaign?> GetSingleById(int id)
        {
            var campaigns = await _context.Set<Campaign>()
                            .Include(c => c.Partner)
                            .Include(c => c.CampaignImages)
                            .Include(c => c.CampaignGames)
                            .Where(c => c.Id == id).ToListAsync();
            if (!campaigns.Any())
            {
                return null;
            }
            return campaigns[0];
        }
    }
}