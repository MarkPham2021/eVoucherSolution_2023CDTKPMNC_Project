using eVoucher_Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_ViewModel.Requests.VoucherRequests
{
    public class VoucherVM
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int AppUserId { get; set; }
        public string AppUserName { get; set; }
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }
        public int PartnerCategoryId { get; set; }
        public string PartnerCategoryName { get; set; }
        public string PartnerAddress { get; set; }
        public string PartnerPhoneNumber { get; set; }
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }
        public string VoucherTypeName { get; set; }
        public int DiscountRate { get; set; }
        public string Promotion { get; set; }
        public DateTime DateGot { get; set; }

        public DateTime ExpiringDate { get; set; }
        public VoucherStatus VoucherStatus { get; set; }
        public string? VoucherTypeImagePath { get; set; }
    }
}
