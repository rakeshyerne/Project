using AR_VehicleServiceManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace AR_VehicleServiceManagement.Data
{
    public class VehicleServiceContext : DbContext
    {
        public VehicleServiceContext(DbContextOptions<VehicleServiceContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Register> registers { get; set; }
        

        internal async Task SearchAppointmentsAsync(string? userName, string? vehicleLicensePlate)
        {
            throw new NotImplementedException();
        }
    }
}
