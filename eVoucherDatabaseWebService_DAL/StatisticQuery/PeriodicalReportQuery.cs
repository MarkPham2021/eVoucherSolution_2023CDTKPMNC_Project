﻿using Azure.Core;
using eVoucher_Utility.Enums;
using eVoucher_ViewModel.StatisticVM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            periodicalReport.CategoryData = new List<CategoryData>();
            //get data for periodicalReport.Data
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
                var numberofcustomers = await _context.Customers
                            .Where(x => x.CreatedTime.Date <= p.Date && x.IsDeleted == false && x.Status == ActiveStatus.Active)
                            .CountAsync();
                var numberofnewcustomers = 0;                
                var daysinoneperiod = (periods[1] - periods[0]).Days;
                var dateprevious = p.AddDays(-daysinoneperiod);
                numberofnewcustomers = await _context.Customers
                        .Where(x => x.CreatedTime.Date <= p.Date && x.CreatedTime.Date >= dateprevious && x.IsDeleted == false && x.Status == ActiveStatus.Active)
                        .CountAsync();
                int numberofpartners = 0;
                int numberofactivecampaigns = 0;
                int numberofdeliveredvouchers = 0;
                if (request.CategoryId == 0) //all categories
                {
                    numberofpartners = await _context.Partners
                        .Where(x => x.CreatedTime <= p && x.IsDeleted == false && x.Status == ActiveStatus.Active)
                        .CountAsync();
                    numberofactivecampaigns = await _context.Campaigns
                            .Where(x => x.CreatedTime <= p && x.EndingDate >= p &&
                                x.IsDeleted == false && x.Status == ActiveStatus.Active)
                            .CountAsync();
                    
                    numberofdeliveredvouchers = await _context.Vouchers
                            .Where(x => x.DateGet <= p)
                            .CountAsync();
                    
                }
                else //specified a category
                {
                    numberofpartners = await _context.Partners                        
                        .Where(x => x.CreatedTime <= p && x.IsDeleted == false 
                            && x.Status == ActiveStatus.Active && x.Partnercategory.Id ==request.CategoryId)
                        .CountAsync();
                    numberofactivecampaigns = await _context.Campaigns
                            .Include(c => c.Partner)
                            .Where(x => x.CreatedTime <= p && x.EndingDate >= p && x.IsDeleted == false 
                                && x.Status == ActiveStatus.Active && x.Partner.Partnercategory.Id == request.CategoryId)
                            .CountAsync();
                    
                    numberofdeliveredvouchers = await _context.Vouchers
                            .Include(v=> v.GamePlayResult)
                            .ThenInclude(GamePlayResult =>  GamePlayResult.CampaignGame)
                            .ThenInclude(CampaignGame => CampaignGame.Campaign)
                            .ThenInclude(Campaign => Campaign.Partner)
                            .Where(x => x.DateGet <= p && x.GamePlayResult.CampaignGame.Campaign.Partner.Partnercategory.Id == request.CategoryId)
                            .CountAsync();
                }
                var d = new DateData()
                {
                    DateReport = p.ToShortDateString(),
                    NumberOfPartners = numberofpartners,
                    NumberOfActiveCampaigns = numberofactivecampaigns,
                    NumberOfCustomers = numberofcustomers,
                    NumberOfNewCustomers = numberofnewcustomers,
                    NumberOfDeliveredVouchers = numberofdeliveredvouchers
                };
                periodicalReport.Data.Add(d);
            }
            //get data for periodicalReport.CategoryData
            var categoriesview = await _context.PartnerCategories.Where(x=> x.Status == ActiveStatus.Active).ToListAsync();
            foreach(var _category in categoriesview)
            {
                var catdata = new CategoryData()
                {
                    CategoryName = _category.Name,
                    NumberOfPartners = await _context.Partners
                        .Where(x => x.IsDeleted == false && x.Status == ActiveStatus.Active 
                            && x.Partnercategory.Id == _category.Id)
                        .CountAsync(),
                    NumberOfActiveCampaigns = await _context.Campaigns
                            .Include(c => c.Partner)
                            .Where(x => x.IsDeleted == false && x.Status == ActiveStatus.Active
                                && x.EndingDate >= DateTime.Now && x.Partner.Partnercategory.Id == _category.Id)
                            .CountAsync(),
                    NumberOfDeliveredVouchers = await _context.Vouchers
                            .Include(v => v.GamePlayResult)
                            .ThenInclude(GamePlayResult => GamePlayResult.CampaignGame)
                            .ThenInclude(CampaignGame => CampaignGame.Campaign)
                            .ThenInclude(Campaign => Campaign.Partner)
                            .Where(x => x.GamePlayResult.CampaignGame.Campaign.Partner.Partnercategory.Id == _category.Id)
                            .CountAsync()
                };
                periodicalReport.CategoryData.Add(catdata);
            }
            return periodicalReport;
        }
        public async Task<PartnerPeriodicalReport> GetPartnerPeriodicalReport(PartnerCreatePeriodicalReportRequest request)
        {
            var partnerperiodicalReport = new PartnerPeriodicalReport();
            partnerperiodicalReport.Data = new List<PartnerDateData>();
            //get data for partnerperiodicalReport.Data
            var periods = new List<DateTime>();
            var lastperiod = request.LastPeriod;

            if (request.PeriodicalType == 1)//daily report
            {
                for (int i = request.NumberOfPeriods; i > 0; i--)
                {
                    var p = lastperiod.AddDays(1 - i);
                    periods.Add(p);
                }

            }
            else if (request.PeriodicalType == 2)//weekly report
            {
                for (int i = request.NumberOfPeriods; i > 0; i--)
                {
                    var p = lastperiod.AddDays((1 - i) * 7);
                    periods.Add(p);
                }

            }
            else if (request.PeriodicalType == 3)
            {
                for (int i = request.NumberOfPeriods; i > 0; i--)
                {
                    var p = lastperiod.AddMonths(1 - i);
                    periods.Add(p);
                }

            }
            else if (request.PeriodicalType == 4)
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
                var daysinoneperiod = (periods[1] - periods[0]).Days;
                var dateprevious = p.AddDays(-daysinoneperiod);
                //numberofnewcustomers = await _context.Customers
                //        .Where(x => x.CreatedTime.Date <= p.Date && x.CreatedTime.Date >= dateprevious && x.IsDeleted == false && x.Status == ActiveStatus.Active)
                //        .CountAsync();
                
                int numberofactivecampaigns = 0;
                int numberofendedcampaigns = 0;
                int numberofdeliveredvouchers = 0;
                int numberofvouchersdeliveredinperiod = 0;
                if (request.CampaignId == 0) //all campaigns
                {
                    numberofactivecampaigns = await _context.Campaigns
                            .Where(x => x.CreatedTime <= p && x.IsDeleted == false && x.Status == ActiveStatus.Active 
                            && x.EndingDate >= p && x.CreatedBy ==request.UserInfo)
                            .CountAsync();
                    numberofendedcampaigns = await _context.Campaigns
                            .Where(x => x.IsDeleted == false && x.EndingDate<p
                            && x.CreatedBy == request.UserInfo)
                            .CountAsync();
                    numberofdeliveredvouchers = await _context.Vouchers
                            .Include(v=>v.GamePlayResult)
                            .ThenInclude(g=>g.CampaignGame)
                            .ThenInclude(c=> c.Campaign)
                            .Where(x => x.DateGet <= p && x.GamePlayResult.CampaignGame.Campaign.CreatedBy == request.UserInfo)
                            .CountAsync();
                    numberofvouchersdeliveredinperiod = await _context.Vouchers
                            .Include(v => v.GamePlayResult)
                            .ThenInclude(g => g.CampaignGame)
                            .ThenInclude(c => c.Campaign)
                            .Where(x => x.DateGet.Date <= p.Date && x.DateGet.Date >= dateprevious 
                            && x.GamePlayResult.CampaignGame.Campaign.CreatedBy == request.UserInfo)
                            .CountAsync();
                }
                else //specified a campaign
                {
                    numberofactivecampaigns = await _context.Campaigns
                            .Where(x => x.CreatedTime <= p && x.IsDeleted == false && x.Status == ActiveStatus.Active 
                            && x.EndingDate >= p && x.CreatedBy == request.UserInfo)
                            .CountAsync();
                    numberofendedcampaigns = await _context.Campaigns
                            .Where(x => x.IsDeleted == false && x.EndingDate < p
                            && x.CreatedBy == request.UserInfo)
                            .CountAsync();
                    numberofdeliveredvouchers = await _context.Vouchers
                            .Include(v => v.GamePlayResult)
                            .ThenInclude(g => g.CampaignGame)
                            .ThenInclude(c => c.Campaign)
                            .Where(x => x.DateGet <= p && x.GamePlayResult.CampaignGame.Campaign.Id == request.CampaignId)
                            .CountAsync();
                    numberofvouchersdeliveredinperiod = await _context.Vouchers
                            .Include(v => v.GamePlayResult)
                            .ThenInclude(g => g.CampaignGame)
                            .ThenInclude(c => c.Campaign)
                            .Where(x => x.DateGet.Date <= p.Date && x.DateGet.Date >= dateprevious
                            && x.GamePlayResult.CampaignGame.Campaign.Id == request.CampaignId)
                            .CountAsync();
                }
                var d = new PartnerDateData()
                {
                    DateReport = p.ToShortDateString(),
                    NumberOfEndedCampaigns = numberofendedcampaigns,
                    NumberOfActiveCampaigns = numberofactivecampaigns,
                    NumberOfDeliveredVouchers = numberofdeliveredvouchers,
                    NumberOfVouchersDeliveredInPeriod = numberofvouchersdeliveredinperiod
                };
                partnerperiodicalReport.Data.Add(d);
            }            
            return partnerperiodicalReport;
        }
    }
}
