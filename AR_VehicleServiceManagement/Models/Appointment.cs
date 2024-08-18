using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AR_VehicleServiceManagement.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; } // Changed from string to int

        [Required]
        public string VehicleNumber { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string VehicleModel { get; set; }

        [Required]
        public string VehicleType { get; set; }

        [Range(1900, 2100)]
        public int VehicleYear { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public string ServiceType { get; set; }

        [Required]
        public TimeOnly AppointmentTime { get; set; }
        public User User { get; set; }

    }
}
