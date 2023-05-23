using eVoucher_DAL.InfraStructure;
using eVoucher_DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_DAL.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
    }
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(eVoucherDbContext context) : base(context)
        {
        }
    }
}

