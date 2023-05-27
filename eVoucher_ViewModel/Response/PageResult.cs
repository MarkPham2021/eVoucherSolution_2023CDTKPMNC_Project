using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_ViewModel.Response
{
    public class PageResult<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get
            {
                var n = (double)TotalItems / PageSize;
                return (int)Math.Ceiling(n);
            }
        }
        public List<T> Items { get; set; }
    }
}
