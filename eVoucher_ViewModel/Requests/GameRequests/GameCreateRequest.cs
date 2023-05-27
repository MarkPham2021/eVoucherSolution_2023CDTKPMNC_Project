using eVoucher_Utility.Enums;

namespace eVoucher_ViewModel.Requests.GameRequests
{
    public class GameCreateRequest
    {
        public string Name { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public int IsDeleted { get; set; } = 0;
        public ActiveStatus Status { get; set; } = ActiveStatus.Active;
    }
}