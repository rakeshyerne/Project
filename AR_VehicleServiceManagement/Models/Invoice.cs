using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AR_VehicleServiceManagement.Models
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceId { get; set; }

        // Foreign key for Appointment
        public int AppointmentId { get; set; }

        // Foreign key for User
        public int UserId { get; set; }

        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }

        // Calculated property for GrandTotal
        public decimal GrandTotal => TotalAmount + Tax - Discount;

        public bool IsPaid { get; set; }

        // Navigation properties
        public User UserID { get; set; }
        public Appointment Appointment { get; set; }
    }
}
