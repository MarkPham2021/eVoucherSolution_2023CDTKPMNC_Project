using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVoucher_BUS.Requests.PartnerRequests
{
    public class PartnerEditRequest
    {
        [Display(Name = "Tên Đối tác: ")]
        public string Name { get; set; }
        [Display(Name = "Lĩnh vực kinh doanh: ")]
        public int PartnerCategoryID { get; set; }
        [Display(Name = "Địa chỉ: ")]
        public string Address { get; set; }
        [Display(Name = "Số điện thoại: ")]
        public string Phone { get; set; }
        public string Email { get; set; }
        [Display(Name = "Tên đăng nhập: ")]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu: ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Xác nhận mật khẩu: ")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
