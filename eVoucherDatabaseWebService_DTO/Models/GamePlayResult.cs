using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_DTO.Models
{
    public class GamePlayResult
    {
        [Key]
        public int Id { get; set; }        
        [ForeignKey("CampaignGameID")]
        public CampaignGame CampaignGame { get; set; }
        public int GotNumberResult { get; set; }
        public bool IsGotVoucher { get; set; }
        [ForeignKey("VoucherTypeID")]
        public VoucherType? VoucherType { get; set; }
        public string? Description { get; set; }
        public  Voucher? Voucher { set; get; }
        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }
    }
}
