using eVoucher_BUS.Requests.VoucherRequests;
using Microsoft.AspNetCore.Http;

namespace eVoucher_BUS.Requests.CampaignRequests
{
    public class CampaignVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }
        public string PartnerCategoryName { get; set; }
        public string? Slogan { get; set; }
        public string? MetaKeyword { get; set; }
        public string? MetaDescription { get; set; }
        public DateTime BeginningDate { get; set; }
        public DateTime EndingDate { get; set; }
        public bool HomeFlag { get; set; }
        public bool HotFlag { get; set; }
        public List<VoucherTypeVM> VoucherTypes { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public string? ImagePath { get; set; }
    }
}