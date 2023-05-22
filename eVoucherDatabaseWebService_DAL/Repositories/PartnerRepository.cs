using eVoucher_DAL.InfraStructure;
using eVoucher_DTO.Models;

namespace eVoucher_DAL.Repositories
{
    public interface IPartnerRepository : IRepository<Partner>
    {
    }

    public class PartnerRepository : RepositoryBase<Partner>, IPartnerRepository
    {
        public PartnerRepository(eVoucherDbContext context) : base(context)
        {
        }
    }
}