using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Utilities.Constants
{
    public class SystemConstants
    {
        public const string MainConnectionString = "DefaultConnection";        

        public class AppSettings
        {            
            public const string Token = "Token";
            public const string BaseAddress = "BaseAddress";
        }

        public class CampaignSettings
        {
            public const int NumberOfFeaturedCampaigns = 4;
            public const int NumberOfLatestCampaigns = 6;
        }        
    }
}
