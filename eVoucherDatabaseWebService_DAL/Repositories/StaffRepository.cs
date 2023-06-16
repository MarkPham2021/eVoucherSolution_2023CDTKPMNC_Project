using eVoucher_DAL.InfraStructure;
using eVoucher_ViewModel.Requests.PartnerRequests;

namespace eVoucher_DAL.Repositories
{
    public interface IStaffRepository : IRepository<Staff>
    {
        Task<Staff> Activate(int id);
        Task<Staff> Lock(int id);
    }

    public class StaffRepository : RepositoryBase<Staff>, IStaffRepository
    {
        public StaffRepository(eVoucherDbContext context) : base(context)
        {
        }

        public async Task<Staff> Activate(int id)
        {
            var activateStaff = await _context.Database.ExecuteSqlAsync($"UPDATE [dbo].[Staffs] SET [Status] = 1 WHERE [Id] = {id}");
            var staff = await this.GetSingleByCondition(s => s.Id == id, includes: new string[] { "AppUser" });
            return staff;
        }
        public async Task<Staff> Lock(int id)
        {
            var lockStaff = await _context.Database.ExecuteSqlAsync($"UPDATE [Staffs] SET [Status] = 0 WHERE [Id] = {id}");
            var staff = await GetSingleByCondition(s => s.Id == id, includes: new string[] { "AppUser" });               
            return staff;
        }
    }
}