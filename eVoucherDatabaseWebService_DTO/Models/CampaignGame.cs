using eVoucher_DTO.Abtracts;

namespace eVoucher_DTO.Models
{
    public class CampaignGame : RootClass
    {
        [ForeignKey("GameID")]
        public virtual Game Game { set; get; }
        [ForeignKey("CampaignID")]
        public virtual Campaign Campaign { set; get; }
    }
}