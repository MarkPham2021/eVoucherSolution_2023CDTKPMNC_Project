namespace eVoucher_ViewModel.Requests.VoucherRequests
{
    public class GetCustomerVouchersRequestPaging
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
        public string? Keyword { get; set; }
    }
}