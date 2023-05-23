using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_BUS.Requests.UserRequests
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }
        public bool Rememberme { get; set; }
    }
}
