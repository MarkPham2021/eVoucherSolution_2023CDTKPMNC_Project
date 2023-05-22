using eVoucher_DTO.Abtracts;

namespace eVoucher_DTO.Models
{
    [Table("PartnerCategories")]
    public class PartnerCategory : RootClass
    {
        public virtual IEnumerable<Partner> Partners { set; get; }
    }
}