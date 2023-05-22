using eVoucher_DTO.Abtracts;

namespace eVoucher_DTO.Models
{
    [Table("Campaigns")]
    public class Campaign : RootClass
    {
        [MaxLength(250)]
        public string? Slogan { get; set; }
        [MaxLength(256)]
        public string? MetaKeyword { set; get; }

        [MaxLength(256)]
        public string? MetaDescription { set; get; }
        [Required]
        public DateTime BeginningDate { get; set; }
        [Required]
        public DateTime EndingDate { get; set; }
        [Required]
        public bool HomeFlag { get; set; }
        [Required]
        public bool HotFlag { get; set; }
        [ForeignKey("PartnerID")]
        public virtual Partner Partner { get; set; }
        public virtual IEnumerable<CampaignGame>? CampaignGames { set; get; }
        public List<CampaignImage>? CampaignImages { set; get; }
    }
}