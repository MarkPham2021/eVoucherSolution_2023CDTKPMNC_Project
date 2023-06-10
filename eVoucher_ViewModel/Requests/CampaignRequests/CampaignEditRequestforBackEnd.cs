using eVoucher_ViewModel.Requests.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_ViewModel.Requests.CampaignRequests
{
    public class CampaignEditRequestforBackEnd
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Slogan { get; set; }
        public string? MetaKeyword { get; set; }
        public string? MetaDescription { get; set; }
        public DateTime BeginningDate { get; set; }
        public DateTime EndingDate { get; set; }
        public bool HomeFlag { get; set; }
        public bool HotFlag { get; set; }
        public string Games { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; } = DateTime.Now;
        public IFormFile? ImageFile { get; set; }
    }
}
