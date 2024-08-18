using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using AR_VehicleServiceManagement.Models;
using AR_VehicleServiceManagement.Data;
using Microsoft.AspNetCore.Authorization;

namespace VehicleServiceCenter.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly VehicleServiceContext _context;

        public AppointmentsController(VehicleServiceContext context)
        {
            _context = context;
        }

        // GET: api/Appointments
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments([FromQuery] string? username = null, [FromQuery] string? vehicleLicensePlate = null)
        {
            var query = _context.Appointments.Include(a => a.User).AsQueryable();

            // Filter by username if provided
            if (!string.IsNullOrEmpty(username))
            {
                query = query.Where(a => a.User.Username.Contains(username));
            }

            // Filter by vehicleLicensePlate if provided
            if (!string.IsNullOrEmpty(vehicleLicensePlate))
            {
                query = query.Where(a => a.VehicleNumber.Contains(vehicleLicensePlate));
            }

            var appointments = await query.ToListAsync();
            return Ok(appointments);
        }

        // GET: api/Appointments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            var appointment = await _context.Appointments.Include(a => a.User).FirstOrDefaultAsync(a => a.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound(new { Message = $"Appointment with id {id} not found." });
            }

            return Ok(appointment);
        }

        // POST: api/Appointments
        [HttpPost]
        public async Task<ActionResult<Appointment>> CreateAppointment(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.AppointmentId }, appointment);
        }

        // PUT: api/Appointments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return BadRequest(new { Message = "Appointment ID mismatch." });
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return NotFound(new { Message = $"Appointment with id {id} not found." });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Appointments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound(new { Message = $"Appointment with id {id} not found." });
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Utility method to check if an appointment exists
        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.AppointmentId == id);
        }
    }
}
