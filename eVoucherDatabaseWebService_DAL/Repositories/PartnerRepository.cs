using eVoucher_DAL.InfraStructure;
using eVoucher_DTO.Models;
using eVoucher_ViewModel.Requests.PartnerRequests;
using System.Linq.Expressions;

namespace eVoucher_DAL.Repositories
{
    public interface IPartnerRepository : IRepository<Partner>
    {
        Task<PartnerVM?> GetPartnerWithAppUserByCondition(Expression<Func<Partner, bool>> predicate);
    }

    public class PartnerRepository : RepositoryBase<Partner>, IPartnerRepository
    {
        public PartnerRepository(eVoucherDbContext context) : base(context)
        {
        }
        public async Task<PartnerVM?> GetPartnerWithAppUserByCondition(Expression<Func<Partner, bool>> predicate)
        {
            var partners =  await _context.Set<Partner>()
                .Include(p=>p.AppUser)
                .Include(p=>p.Partnercategory)
                .Include(p=>p.PartnerImages)
                .Where(predicate).AsQueryable().ToListAsync();
            if (!partners.Any() )
            {
                return null;
            }
            var partnervm = new PartnerVM()
            {
                Partner = partners[0],                
                PartnerCategoryName = partners[0].Partnercategory.Name
            };
            return partnervm;
        }
    }
}