using eVoucher_DTO.Abtracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_DTO.Models
{

    [Table("VoucherTypeImages")]
    public class VoucherTypeImage : ObjectImage
    {
        [ForeignKey("VoucherTypeID")]
        public VoucherType VoucherType { get; set; }
    }
}
