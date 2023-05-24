using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace eVoucher_BUS.Requests.PartnerRequests
{
    public class PartnerCreateRequest
    {
        
        public string Name { get; set; }
        
        public int PartnerCategoryID { get; set; }
       
        public string Address { get; set; }
        
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public int UserTypeId { get; set; } = 2;
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public IFormFile? ImageFile { get; set; }
    }
}