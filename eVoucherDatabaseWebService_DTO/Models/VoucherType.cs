using eVoucher_DTO.Abtracts;

namespace eVoucher_DTO.Models
{
    public class VoucherType : RootClass
    {
        [ForeignKey("CampaignID")]
        public virtual Campaign Campaign { set; get; }

        public int DiscountRate { get; set; }
        public int Presents { get; set; }

        [Column(TypeName = "nvarchar")]
        public string? Promotion { get; set; }

        public DateTime ExpiringDate { get; set; }
        public int MaxAmount { get; set; }
        public int RemainAmount { get; set; }
        public ICollection<Voucher> Vouchers { get; set; }
    }
}