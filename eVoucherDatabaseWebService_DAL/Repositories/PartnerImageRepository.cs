using eVoucher_DAL.InfraStructure;

namespace eVoucher_DAL.Repositories
{
    public interface IPartnerImageRepository : IRepository<PartnerImage>
    { }

    public class PartnerImageRepository : RepositoryBase<PartnerImage>, IPartnerImageRepository
    {
        public PartnerImageRepository(eVoucherDbContext context) : base(context)
        {
        }
    }
}