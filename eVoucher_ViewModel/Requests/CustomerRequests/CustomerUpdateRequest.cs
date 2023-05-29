using eVoucher_DTO.Models;
using eVoucher_Utility.Enums;

namespace eVoucher_ViewModel.Requests.CustomerRequests
{
    public class CustomerUpdateRequest
    {
        AppUser AppUser { get; set; }
        Customer Customer { get; set; }
        public string Password { get; set; }
    }
}