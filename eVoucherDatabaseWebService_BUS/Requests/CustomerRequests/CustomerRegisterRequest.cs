using eVoucher_Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_BUS.Requests.CustomerRequests
{
    public class CustomerRegisterRequest
    {
        public string Name { get; set; }
        public Sex Gender { get; set; }
        public DateTime DOB { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ConfirmEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public int UserTypeId { get; set; }
    }
}
