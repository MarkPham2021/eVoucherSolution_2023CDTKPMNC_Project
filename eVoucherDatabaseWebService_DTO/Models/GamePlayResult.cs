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
        public virtual CampaignGame CampaignGame { get; set; }
        public int Result { get; set; }
        public bool IsGotVoucher { get; set; }
        public string? Description { get; set; }
        public virtual IEnumerable<Voucher>? Vouchers { set; get; }

    }
}
