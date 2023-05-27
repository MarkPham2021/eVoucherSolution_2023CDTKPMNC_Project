using eVoucher_DAL.InfraStructure;
using eVoucher_ViewModel.Requests.CampaignRequests;

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
                    ImagePath = vm.CampaignImages[0].ImagePath
                };
                campaignvmlist.Add(item);
            }
            return campaignvmlist;
        }
    }
}