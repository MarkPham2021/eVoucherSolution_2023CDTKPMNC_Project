using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_ViewModel.Requests.Common
{
    public class GetGoogleDistanceMatrixRequest
    {
        public string? destinations {  get; set; }
        public string? origins { get; set; }        
        public string key { get; set; }
    }
}
