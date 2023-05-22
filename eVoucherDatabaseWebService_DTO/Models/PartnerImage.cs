using eVoucher_DTO.Abtracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_DTO.Models
{
    [Table("PartnerImages")]
    public class PartnerImage : ObjectImage
    {
        [ForeignKey("PartnerID")]
        public Partner Partner { get; set; }
    }
}
