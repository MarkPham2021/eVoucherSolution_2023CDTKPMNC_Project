using eVoucher_DAL.InfraStructure;


namespace eVoucher_DAL.Repositories
{
    public interface IVoucherTypeImageRepository : IRepository<VoucherTypeImage> { }
    public class VoucherTypeImageRepository : RepositoryBase<VoucherTypeImage>, IVoucherTypeImageRepository
    {
        public VoucherTypeImageRepository(eVoucherDbContext context) : base(context) { }
    }
}
