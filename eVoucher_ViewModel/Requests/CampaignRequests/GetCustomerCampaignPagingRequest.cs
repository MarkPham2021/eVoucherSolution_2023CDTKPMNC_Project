using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_ViewModel.Requests.CampaignRequests
{
    public class GetCustomerCampaignPagingRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
        public string? keyword { get; set; }
        public int? filter { get; set; }
        public int? categoryId { get; set; }
        public string? currentAddress { get; set; }
    }
}
