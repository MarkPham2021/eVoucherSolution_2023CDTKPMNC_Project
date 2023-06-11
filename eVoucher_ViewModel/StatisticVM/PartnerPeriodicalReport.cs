using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_ViewModel.StatisticVM
{
    public class PartnerCreatePeriodicalReportRequest
    {
        public string UserInfo { get; set; }
        public string Keyword { get; set; }
        public int PeriodicalType { get; set; }
        public int NumberOfPeriods { get; set; }
        public int CampaignId { get; set; }
        public DateTime LastPeriod { get; set; }
    }
    public class PartnerDateData
    {
        public string DateReport { get; set; }
        public int NumberOfActiveCampaigns { get; set; }
        public int NumberOfEndedCampaigns { get; set; }        
        public int NumberOfDeliveredVouchers { get; set; }
        public int NumberOfVouchersDeliveredInPeriod { get; set; }
    }   
    public class PartnerPeriodicalReport
    {
        public List<PartnerDateData> Data { get; set; }
    }
}
