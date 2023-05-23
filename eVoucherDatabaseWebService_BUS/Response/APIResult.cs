using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_BUS.Response
{
    public class APIResult<T>
    {
        public bool IsSucceeded { get; set; }
        public string Message { get; set; }
        public T ResultObj { get; set; }
        public APIResult() 
        { 
            IsSucceeded = true;                      
        }
        public APIResult(bool s,string m, T obj)
        {
            IsSucceeded = s;
            Message = m;
            ResultObj = obj;
        }
    }
}
