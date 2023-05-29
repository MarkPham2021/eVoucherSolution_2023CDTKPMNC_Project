using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_ViewModel.Requests.CustomerRequests
{
    public class CustomerPlayGameForVoucherRequest
    {
        public string AppUserInfo { get; set; } //get from session and send User.Identity.Name to backend
        public int CampaignGameId { get; set; }
        public int GottenNumber { get; set;} //send the random number customer got after play game
    }
}
