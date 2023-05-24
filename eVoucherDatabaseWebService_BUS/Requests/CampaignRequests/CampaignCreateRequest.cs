using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_BUS.Requests.CampaignRequests
{
    public class CampaignCreateRequest
    {
        public int PartnerID { get; set; }
        public string Name { get; set; }
        public string? Slogan { get; set; }
        public string? MetaKeyword { get; set; }
        public string? MetaDescription { get; set; }
        public DateTime BeginningDate { get; set; }
        public DateTime EndingDate { get; set; }
        public bool HomeFlag { get; set; }
        public bool HotFlag { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public IFormFile? ImageFile { get; set; }
    }
}
