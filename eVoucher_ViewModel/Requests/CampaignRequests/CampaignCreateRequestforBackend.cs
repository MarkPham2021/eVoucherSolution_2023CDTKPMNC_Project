using Microsoft.AspNetCore.Http;

namespace eVoucher_ViewModel.Requests.CampaignRequests
{
    public class CampaignCreateRequestforBackend
    {
        public int PartnerAppUserId { get; set; }
        public string Name { get; set; }
        public string? Slogan { get; set; }
        public string? MetaKeyword { get; set; }
        public string? MetaDescription { get; set; }
        public DateTime BeginningDate { get; set; }
        public DateTime EndingDate { get; set; }
        public bool HomeFlag { get; set; }
        public bool HotFlag { get; set; }
        public string Games { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public IFormFile? ImageFile { get; set; }
    }
}