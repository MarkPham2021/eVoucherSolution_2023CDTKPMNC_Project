using eVoucher_DTO.Models;
using eVoucher_Utility.Enums;

namespace eVoucher_ViewModel.Requests.CustomerRequests
{
    public class CustomerUpdateRequest
    {
        public AppUser AppUser { get; set; }
        public Customer Customer { get; set; }
        public string Password { get; set; }
    }
}