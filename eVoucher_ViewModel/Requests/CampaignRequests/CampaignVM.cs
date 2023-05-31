using eVoucher_Utility.Enums;
using eVoucher_ViewModel.Requests.GameRequests;
using eVoucher_ViewModel.Requests.VoucherRequests;

namespace eVoucher_ViewModel.Requests.CampaignRequests
{
    public class CampaignVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }       
        public string PartnerCategoryName { get; set; }
        public string PartnerPhoneNumber { get; set; }
        public string PartnerAddress { get; set; }
        public string? PartnerImagePath { get; set; }
        public string? Slogan { get; set; }
        public string? MetaKeyword { get; set; }
        public string? MetaDescription { get; set; }
        public DateTime BeginningDate { get; set; }
        public DateTime EndingDate { get; set; }
        public bool HomeFlag { get; set; }
        public bool HotFlag { get; set; }
        public List<VoucherTypeVM>? VoucherTypes { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
        public ActiveStatus Status { get; set; }
        public string? ImagePath { get; set; }
        public List<CampaignGameVM>? campaignGames { get; set; }
    }
}