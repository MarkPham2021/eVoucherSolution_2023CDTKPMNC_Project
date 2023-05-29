using eVoucher_DTO.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_ViewModel.Requests.PartnerRequests
{
    public class PartnerEditRequest
    {
        Partner Partner { get; set; }
        AppUser User { get; set; }
        public string Password { get; set; }
        public IFormFile? NewImageFile { get; set; }
        
    }
}
