using eVoucher_DAL.InfraStructure;


namespace eVoucher_DAL.Repositories
{
    public interface IVoucherRepository : IRepository<Voucher> { }
    public class VoucherRepository : RepositoryBase<Voucher>, IVoucherRepository
    {
        public VoucherRepository(eVoucherDbContext dbContext) : base(dbContext) { }
    }
}

//VoucherRepository