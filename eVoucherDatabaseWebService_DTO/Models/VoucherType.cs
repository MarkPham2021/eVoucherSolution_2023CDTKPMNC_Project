using eVoucher_DTO.Abtracts;

namespace eVoucher_DTO.Models
{
    public class VoucherType : RootClass
    {
        [ForeignKey("CampaignID")]
        public Campaign Campaign { set; get; }

        public int DiscountRate { get; set; }        
        [Column(TypeName = "nvarchar(max)")]
        public string? Promotion { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string LuckyNumbers { get; set; }

        public DateTime ExpiringDate { get; set; }
        public int MaxAmount { get; set; }
        public int RemainAmount { get; set; }
        public List<VoucherTypeImage>? VoucherTypeImages { get; set; }
        public List<Voucher>? Vouchers { get; set; }
    }
}