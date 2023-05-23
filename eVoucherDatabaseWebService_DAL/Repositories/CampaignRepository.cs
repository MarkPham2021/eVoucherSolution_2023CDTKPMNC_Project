using eVoucher_DAL.InfraStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_DAL.Repositories
{
    public interface ICampaignRepository : IRepository<Campaign>
    {

    }
    public class CampaignRepository : RepositoryBase<Campaign>, ICampaignRepository
    {
        public CampaignRepository(eVoucherDbContext context) : base(context) { }
    }
}
