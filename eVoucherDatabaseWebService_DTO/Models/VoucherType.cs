using eVoucher_DTO.Abtracts;

namespace eVoucher_DTO.Models
{
    public class VoucherType : RootClass
    {
        [ForeignKey("CampaignID")]
        public Campaign Campaign { set; get; }

        public int DiscountRate { get; set; }        
        [Column(TypeName = "nvarchar")]
        public string? Promotion { get; set; }
        public string LuckyNumbers { get; set; }

        public DateTime ExpiringDate { get; set; }
        public int MaxAmount { get; set; }
        public int RemainAmount { get; set; }
        public List<VoucherTypeImage>? VoucherTypeImages { get; set; }
        public ICollection<Voucher> Vouchers { get; set; }
    }
}