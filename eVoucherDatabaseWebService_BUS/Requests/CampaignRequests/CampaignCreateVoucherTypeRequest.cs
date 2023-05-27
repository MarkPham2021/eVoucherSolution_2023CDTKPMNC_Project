using eVoucher_DTO.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_BUS.Requests.CampaignRequests
{
    public class CampaignCreateVoucherTypeRequest
    {
        public int CampaignId { get; set; }
        public string Name { get; set; }
        public int DiscountRate { get; set; }
        public string? Promotion { get; set; }
        public string? LuckyNumberstr { get; set; }
        public bool IsgetLuckyNumbersRandom { get; set;}
        public int NumberofLuckyNumbers { get; set; }
        public int MaxAmount { get; set; }
        public int RemainAmount { get; set; }
        public IFormFile ImageFile { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set;}

    }
}
