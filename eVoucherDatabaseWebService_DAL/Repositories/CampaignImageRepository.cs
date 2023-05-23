using eVoucher_DAL.InfraStructure;

namespace eVoucher_DAL.Repositories
{
    public interface ICampaignImageRepository : IRepository<CampaignImage>
    { }

    public class CampaignImageRepository : RepositoryBase<CampaignImage>, ICampaignImageRepository
    {
        public CampaignImageRepository(eVoucherDbContext context) : base(context)
        {
        }
    }
}