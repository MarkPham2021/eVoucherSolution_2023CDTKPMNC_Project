using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_ViewModel.StatisticVM
{
    public class CreatePeriodicalReportRequest    {
        
        public int PeriodicalType { get; set; }
        public int NumberOfPeriods { get; set; }
        public int CategoryId { get; set; }
        public DateTime LastPeriod { get; set; }

    }
    public class DateData
    {
        public string DateReport { get; set; }
        public int NumberOfPartners { get; set; }
        public int NumberOfActiveCampaigns { get; set;}
        public int NumberOfCustomers { get; set; }
        public int NumberOfNewCustomers { get; set; }
        public int NumberOfDeliveredVouchers { get; set; }
    }
    public class CategoryData
    {
        public string CategoryName { get; set; }
        public int NumberOfPartners { get; set; }
        public int NumberOfActiveCampaigns { get; set; }
        public int NumberOfDeliveredVouchers { get; set; }
    }
    public class PeriodicalReport
    {
        public List<DateData> Data { get; set; }
        public List<CategoryData> CategoryData { get; set; }
    }
}
