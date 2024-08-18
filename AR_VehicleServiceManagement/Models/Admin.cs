using System.ComponentModel.DataAnnotations;

namespace AR_VehicleServiceManagement.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
 
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

    }
}
