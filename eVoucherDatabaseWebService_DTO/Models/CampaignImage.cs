﻿using eVoucherDatabaseWebService_DTO.Abtracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucherDatabaseWebService_DTO.Models
{
    [Table("CampaignImages")]
    public class CampaignImage : ObjectImage
    {
        [ForeignKey("CampaignID")]
        public Campaign Campaign { get; set; }
    }
}
