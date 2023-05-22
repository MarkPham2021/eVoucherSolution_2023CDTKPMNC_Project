using eVoucher_Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_DTO.Models
{
    public class Voucher
    {
        public int Id { get; set; }
        [ForeignKey("GamePlayResultID")]
        public virtual GamePlayResult GamePlayResult { get; set; }
        [ForeignKey("VoucherTypeID")]
        public VoucherType VoucherType { get; set; }
        [Required]
        public DateTime DateGet { get; set; }
        [Required]
        public VoucherStatus VoucherStatus { get; set; }
    }
}
