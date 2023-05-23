using eVoucher_DTO.Abtracts;

namespace eVoucher_DTO.Models
{
    public class Game : RootClass
    {
        public int PlayedCount { get; set; } = 0;
        public int CampaignChosenCount { get; set; } = 0;
        public virtual IEnumerable<CampaignGame> CampaignGames { set; get; }
    }
}