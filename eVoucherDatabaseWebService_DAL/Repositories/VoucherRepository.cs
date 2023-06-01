using Azure.Core;
using eVoucher_DAL.InfraStructure;
using eVoucher_DTO.Models;
using eVoucher_ViewModel.Requests.CampaignRequests;
using eVoucher_ViewModel.Requests.PartnerRequests;
using eVoucher_ViewModel.Requests.VoucherRequests;

namespace eVoucher_DAL.Repositories
{
    public interface IVoucherRepository : IRepository<Voucher>
    {
        Task<List<VoucherVM>?> GetAllVoucherVMsOfCustomerByAppUserId(int userid);

        Task<List<VoucherVM>?> GetAllVoucherVMsOfCustomerByCustomerId(int id);

        Task<VoucherVM?> GetVoucherVMById(int id);
    }

    public class VoucherRepository : RepositoryBase<Voucher>, IVoucherRepository
    {
        public VoucherRepository(eVoucherDbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<List<VoucherVM>?> GetAllVoucherVMsOfCustomerByAppUserId(int userid)
        {
            var vouchervms = new List<VoucherVM>();
            var query = from v in _context.Vouchers
                        join gpr in _context.GamePlayResults on v.GamePlayResult.Id equals gpr.Id
                        join cpg in _context.CampaignGames on gpr.CampaignGame.Id equals cpg.Id
                        join cp in _context.Campaigns on cpg.Campaign.Id equals cp.Id
                        join p in _context.Partners on cp.Partner.Id equals p.Id
                        join pcat in _context.PartnerCategories on p.Partnercategory.Id equals pcat.Id
                        join vt in _context.VoucherTypes on gpr.VoucherType.Id equals vt.Id
                        join vtimg in _context.VoucherTypeImages on vt.Id equals vtimg.VoucherType.Id
                        join u in _context.AppUsers on gpr.AppUser.Id equals u.Id
                        join cus in _context.Customers on u.Id equals cus.AppUsers.Id
                        where gpr.AppUser.Id == userid
                        select new {v,gpr,cpg,cp,p, pcat,vt,vtimg,u,cus };
            vouchervms = await query.Select(x => new VoucherVM()
                        {
                            Id = x.v.Id,
                            CustomerId = x.cus.Id,
                            CustomerName = x.cus.Name,
                            AppUserId = x.u.Id,
                            AppUserName = x.u.UserName,
                            PartnerId = x.p.Id,
                            PartnerName = x.p.Name,
                            PartnerCategoryId = x.pcat.Id,
                            PartnerCategoryName = x.pcat.Name,
                            CampaignId = x.cp.Id,
                            CampaignName = x.cp.Name,
                            DiscountRate = x.vt.DiscountRate,
                            Promotion = x.vt.Promotion,
                            ExpiringDate = x.vt.ExpiringDate,
                            VoucherStatus = x.v.VoucherStatus,
                            VoucherTypeImagePath = x.vtimg.ImagePath
                        }).ToListAsync();
           
            return vouchervms;
        }

        public async Task<List<VoucherVM>?> GetAllVoucherVMsOfCustomerByCustomerId(int id)
        {
            var vouchervms = new List<VoucherVM>();
            var query = from v in _context.Vouchers
                        join gpr in _context.GamePlayResults on v.GamePlayResult.Id equals gpr.Id
                        join cpg in _context.CampaignGames on gpr.CampaignGame.Id equals cpg.Id
                        join cp in _context.Campaigns on cpg.Campaign.Id equals cp.Id
                        join p in _context.Partners on cp.Partner.Id equals p.Id
                        join pcat in _context.PartnerCategories on p.Partnercategory.Id equals pcat.Id
                        join vt in _context.VoucherTypes on gpr.VoucherType.Id equals vt.Id
                        join vtimg in _context.VoucherTypeImages on vt.Id equals vtimg.VoucherType.Id
                        join u in _context.AppUsers on gpr.AppUser.Id equals u.Id
                        join cus in _context.Customers on u.Id equals cus.AppUsers.Id
                        where cus.Id == id
                        select new { v, gpr, cpg, cp, p, pcat, vt, vtimg, u, cus };
            vouchervms = await query.Select(x => new VoucherVM()
            {
                Id = x.v.Id,
                CustomerId = x.cus.Id,
                CustomerName = x.cus.Name,
                AppUserId = x.u.Id,
                AppUserName = x.u.UserName,
                PartnerId = x.p.Id,
                PartnerName = x.p.Name,
                PartnerCategoryId = x.pcat.Id,
                PartnerCategoryName = x.pcat.Name,
                CampaignId = x.cp.Id,
                CampaignName = x.cp.Name,
                DiscountRate = x.vt.DiscountRate,
                Promotion = x.vt.Promotion,
                ExpiringDate = x.vt.ExpiringDate,
                VoucherStatus = x.v.VoucherStatus,
                VoucherTypeImagePath = x.vtimg.ImagePath
            }).ToListAsync();

            return vouchervms;
        }

        public Task<VoucherVM?> GetVoucherVMById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

//VoucherRepository