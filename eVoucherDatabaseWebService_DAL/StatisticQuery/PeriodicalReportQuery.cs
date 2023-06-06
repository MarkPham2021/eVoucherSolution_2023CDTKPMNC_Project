using eVoucher_Utility.Enums;
using eVoucher_ViewModel.StatisticVM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_DAL.StatisticQuery
{
    public class PeriodicalReportQuery : BaseStatisticQuery
    {
        public PeriodicalReportQuery(eVoucherDbContext context): base(context) { }
        public async Task<PeriodicalReport?> GetPeriodicalReport(CreatePeriodicalReportRequest request)
        {
            var periodicalReport = new PeriodicalReport();
            periodicalReport.Data = new List<DateData>();
            var periods = new List<DateTime>();
            var lastperiod =request.LastPeriod;            
            
            if (request.PeriodicalType == 1)//daily report
            {
                for (int i = request.NumberOfPeriods; i> 0; i--)
                {
                    var p = lastperiod.AddDays(1-i);
                    periods.Add(p);
                }
                
            }else if (request.PeriodicalType == 2)//weekly report
            {
                for (int i = request.NumberOfPeriods; i > 0; i--)
                {
                    var p = lastperiod.AddDays((1 - i)*7);
                    periods.Add(p);
                }
                
            }else if(request.PeriodicalType == 3)
            {
                for (int i = request.NumberOfPeriods; i > 0; i--)
                {
                    var p = lastperiod.AddMonths(1 - i);
                    periods.Add(p);
                }
                
            }else if(request.PeriodicalType == 4)
            {
                for (int i = request.NumberOfPeriods; i > 0; i--)
                {
                    var p = lastperiod.AddYears(1 - i);
                    periods.Add(p);
                }
                
            }
            else
            {
                return null;
            }
            foreach (var p in periods)
            {
                if (request.CategoryId == 0) //all categories
                {
                    var numberofpartners = await _context.Partners
                        .Where(x => x.CreatedTime <= p && x.IsDeleted == false && x.Status == ActiveStatus.Active)
                        .CountAsync();
                    var numberofactivecampaigns = await _context.Campaigns
                            .Where(x => x.CreatedTime <= p && x.IsDeleted == false && x.Status == ActiveStatus.Active)
                            .CountAsync();
                    var numberofcustomers = await _context.Customers
                            .Where(x => x.CreatedTime <= p && x.IsDeleted == false && x.Status == ActiveStatus.Active)
                            .CountAsync();
                    var numberofdeliveredvouchers = await _context.Vouchers
                            .Where(x => x.DateGet <= p)
                            .CountAsync();
                    var d = new DateData()
                    {
                        DateReport = p.ToShortDateString(),
                        NumberOfPartners = numberofpartners,
                        NumberOfActiveCampaigns = numberofactivecampaigns,
                        NumberOfCustomers = numberofcustomers,
                        NumberOfDeliveredVouchers = numberofdeliveredvouchers
                    };
                    periodicalReport.Data.Add(d);
                }
                else //specified a category
                {
                    var numberofpartners = await _context.Partners                        
                        .Where(x => x.CreatedTime <= p && x.IsDeleted == false 
                            && x.Status == ActiveStatus.Active && x.Partnercategory.Id ==request.CategoryId)
                        .CountAsync();
                    var numberofactivecampaigns = await _context.Campaigns
                            .Include(c => c.Partner)
                            .Where(x => x.CreatedTime <= p && x.IsDeleted == false 
                                && x.Status == ActiveStatus.Active && x.Partner.Partnercategory.Id == request.CategoryId)
                            .CountAsync();
                    var numberofcustomers = await _context.Customers
                            .Where(x => x.CreatedTime <= p && x.IsDeleted == false && x.Status == ActiveStatus.Active)
                            .CountAsync();
                    var numberofdeliveredvouchers = await _context.Vouchers
                            .Include(v=> v.GamePlayResult)
                            .ThenInclude(GamePlayResult =>  GamePlayResult.CampaignGame)
                            .ThenInclude(CampaignGame => CampaignGame.Campaign)
                            .ThenInclude(Campaign => Campaign.Partner)
                            .Where(x => x.DateGet <= p && x.GamePlayResult.CampaignGame.Campaign.Partner.Partnercategory.Id == request.CategoryId)
                            .CountAsync();
                    var d = new DateData()
                    {
                        DateReport = p.ToShortDateString(),
                        NumberOfPartners = numberofpartners,
                        NumberOfActiveCampaigns = numberofactivecampaigns,
                        NumberOfCustomers = numberofcustomers,
                        NumberOfDeliveredVouchers = numberofdeliveredvouchers
                    };
                    periodicalReport.Data.Add(d);
                }
            }
            return periodicalReport;
        }
    }
}
