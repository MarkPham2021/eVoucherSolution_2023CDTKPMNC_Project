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
        Task<Customer> GetCustomerFullInfoById(int id);
        Task<Customer> GetCustomerFullInfoByUserInfo(string userinfo);
        Task<List<Customer>?> GetAllCustomersFullInfo();
    }
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(eVoucherDbContext context) : base(context)
        {
        }

        public async Task<List<Customer>?> GetAllCustomersFullInfo()
        {
            var data = await _context.Customers
                .Include(c=>c.AppUsers)                
                .ToListAsync();
            return data;
        }

        public async Task<Customer> GetCustomerFullInfoById(int id)
        {
            Customer customer = await _context.Customers
                                .SingleAsync(c => c.Id == id);

            await _context.Entry(customer)
                .Reference(c => c.AppUsers)
                .LoadAsync();

           await _context.Entry(customer)
                .Collection(c => c.GamePlayResults)
                .LoadAsync();
            
            return customer;
        }

        public async Task<Customer> GetCustomerFullInfoByUserInfo(string userinfo)
        {
            int appuserid =int.Parse(userinfo.Split('|')[0]);
            Customer customer = await _context.Customers
                                .SingleAsync(c => c.AppUsers.Id == appuserid);

           await _context.Entry(customer)
                .Reference(c => c.AppUsers)
                .LoadAsync();

           await _context.Entry(customer)
                .Collection(c => c.GamePlayResults)
                .LoadAsync();

            return customer;
        }
    }
}

