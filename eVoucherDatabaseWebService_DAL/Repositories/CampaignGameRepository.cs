using eVoucher_DAL.InfraStructure;

namespace eVoucher_DAL.Repositories
{
    public interface ICampaignGameRepository : IRepository<CampaignGame> { }
    public class CampaignGameRepository : RepositoryBase<CampaignGame>, ICampaignGameRepository
    {
        public CampaignGameRepository(eVoucherDbContext context) : base(context) { }
    }
}
