using Microsoft.AspNetCore.Identity;

namespace eVoucher_DTO.Models
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser<int>
    {
        public int UserTypeId { get; set; }
        public List<GamePlayResult>? GamePlayResults { get; set; }        
    }
}