using eVoucher_DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_BUS.Requests.VoucherRequests
{
    public class VoucherTypeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CampaignId { get; set; }
        public int DiscountRate { get; set; }
        public string Promotion { get; set; }
        public string LuckyNumbers { get; set; }

        public DateTime ExpiringDate { get; set; }
        public int MaxAmount { get; set; }
        public int RemainAmount { get; set; }
        public string? ImagePath { get; set; }
    }
}
