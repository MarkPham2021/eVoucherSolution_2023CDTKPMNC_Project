namespace eVoucher_ViewModel.Requests.CampaignRequests
{
    public class GetAdminCampaignsPagingRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
        public string? Keyword { get; set; }
        public int? CategoryId { get; set; }
    }
}