using eVoucher_BUS.Requests.PartnerRequests;
using eVoucher_DAL.Repositories;
using eVoucher_DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_BUS.Services
{
    public interface IPartnerCategoryService
    {
        IEnumerable<PartnerCategory> GetAllPartnerCategorys();
        Task<List<PartnerCategory>> GetAllPartnerCategoriesAsync();

        Task<PartnerCategory?> GetPartnerCategoryById(int id);
        
    }
    public class PartnerCategoryService : IPartnerCategoryService
    {
        private readonly IPartnerCategoryRepository _partnerCategoryRepository;
        public PartnerCategoryService(IPartnerCategoryRepository partnerCategoryRepository)
        {
            _partnerCategoryRepository = partnerCategoryRepository;
        }

        public IEnumerable<PartnerCategory> GetAllPartnerCategorys()
        {
            var categories = _partnerCategoryRepository.GetAll();
            return categories;
        }
        public async Task<List<PartnerCategory>> GetAllPartnerCategoriesAsync()
        {
            var categories = await _partnerCategoryRepository.GetAllAsync();
            return categories;
        }

        public Task<PartnerCategory?> GetPartnerCategoryById(int id)
        {
            var category = _partnerCategoryRepository.GetSingleById(id);
            return category;
        }
    }
}
