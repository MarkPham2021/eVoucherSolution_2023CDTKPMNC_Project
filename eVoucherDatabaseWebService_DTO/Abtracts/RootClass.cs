global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
using eVoucher_Utility.Enums;

namespace eVoucher_DTO.Abtracts
{
    public abstract class RootClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedTime { get; set; }

        [Required]
        [MaxLength(250)]
        public string CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ActiveStatus Status { get; set; } = ActiveStatus.Active;
    }
}