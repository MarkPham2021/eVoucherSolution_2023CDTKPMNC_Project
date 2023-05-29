using eVoucher_ViewModel.Requests.VoucherRequests;
using eVoucher_DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_ViewModel.Response
{
    public class APIClaimVoucherResult
    {
        public bool IsSuccess { get; set; }
        public bool IsGotVoucher { get; set; }
        public string Message { get; set; }
        public VoucherType? _Voucher { get; set; }
    }
}
