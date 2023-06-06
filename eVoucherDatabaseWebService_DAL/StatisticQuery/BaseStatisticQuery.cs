using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_DAL.StatisticQuery
{
    public class BaseStatisticQuery
    {
        protected eVoucherDbContext _context;
        public BaseStatisticQuery(eVoucherDbContext context)
        {
            _context = context;
        }
        
    }
}
