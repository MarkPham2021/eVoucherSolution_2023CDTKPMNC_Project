using eVoucher_DAL.InfraStructure;

namespace eVoucher_DAL.Repositories
{
    public interface IStaffRepository : IRepository<Staff>
    {
    }

    public class StaffRepository : RepositoryBase<Staff>, IStaffRepository
    {
        public StaffRepository(eVoucherDbContext context) : base(context)
        {
        }
    }
}