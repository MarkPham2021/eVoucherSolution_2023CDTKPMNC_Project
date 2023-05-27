using eVoucher_Utility.Enums;

namespace eVoucher_ViewModel.Requests.CustomerRequests
{
    public class CustomerUpdateRequest
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
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; } = DateTime.Now;
        public int UserTypeId { get; set; }
    }
}