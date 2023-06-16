using eVoucher_Utility.Enums;

namespace eVoucher_ViewModel.Requests.CustomerRequests
{
    public class GetAllCustomersPagingRequest
    {
        public string Keyword { get; set; }
        public ActiveStatus AccountStatus { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}