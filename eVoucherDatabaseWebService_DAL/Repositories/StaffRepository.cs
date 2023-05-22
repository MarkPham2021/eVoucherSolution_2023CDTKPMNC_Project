using eVoucher_DAL.InfraStructure;
using eVoucher_DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_DAL.Repositories
{
    public interface IStaffRepository : IRepository<Staff>
    {
    }
    public class StaffRepository: RepositoryBase<Staff>, IStaffRepository
    {
        public StaffRepository(eVoucherDbContext context) : base(context)
        {
        }
    }
}
