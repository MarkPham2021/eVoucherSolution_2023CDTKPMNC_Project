using eVoucher_DAL.InfraStructure;

namespace eVoucher_DAL.Repositories
{
    public interface IVoucherTypeRepository : IRepository<VoucherType> { }
    public class VoucherTypeRepository : RepositoryBase<VoucherType>, IVoucherTypeRepository
    {
        public VoucherTypeRepository(eVoucherDbContext dbContext) : base(dbContext) { }
    }
}
