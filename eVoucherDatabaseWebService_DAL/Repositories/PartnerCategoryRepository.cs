using eVoucher_DAL.InfraStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_DAL.Repositories
{
    public interface IPartnerCategoryRepository : IRepository<PartnerCategory> { }
    public class PartnerCategoryRepository : RepositoryBase<PartnerCategory>, IPartnerCategoryRepository
    {
        public PartnerCategoryRepository(eVoucherDbContext context) : base(context) { }
    }
}
