using eVoucher_DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_ViewModel.Requests.PartnerRequests
{
    public class PartnerVM
    {
        public Partner Partner { get; set; }        
        public string PartnerCategoryName { get; set; }
    }
}
