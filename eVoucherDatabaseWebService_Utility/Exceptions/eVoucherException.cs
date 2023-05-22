using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_Utility.Exceptions
{
    public class eVoucherException: Exception
    {
        public eVoucherException() { }
        public eVoucherException(string message) : base(message) { }
        public eVoucherException(string message, Exception innerException) : base(message, innerException) { }

    }
}
