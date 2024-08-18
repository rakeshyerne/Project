using AR_VehicleServiceManagement.Data;
using AR_VehicleServiceManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AR_VehicleServiceManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly VehicleServiceContext _context;

        public InvoiceController(VehicleServiceContext context)
        {
            _context = context;
        }

        // GET: api/Invoice
        [HttpGet]
        [AllowAnonymous] // This allows unauthenticated users to get the list of invoices
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices([FromQuery] int? userId = null, [FromQuery] int? appointmentId = null)
        {
            var query = _context.Invoices
                .Include(i => i.User)
                .Include(i => i.Appointment)
                .AsQueryable();

            // Filter by UserId if provided
            if (userId.HasValue)
            {
                query = query.Where(i => i.UserId == userId.Value);
            }

            // Filter by AppointmentId if provided
            if (appointmentId.HasValue)
            {
                query = query.Where(i => i.AppointmentId == appointmentId.Value);
            }

            var invoices = await query.ToListAsync();
            return Ok(invoices);
        }

        // GET: api/Invoice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.User)
                .Include(i => i.Appointment)
                .FirstOrDefaultAsync(i => i.InvoiceId == id);

            if (invoice == null)
            {
                return NotFound(new { Message = $"Invoice with id {id} not found." });
            }

            return Ok(invoice);
        }

        // POST: api/Invoice
        [HttpPost]
        public async Task<ActionResult<Invoice>> CreateInvoice([FromBody] Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInvoice), new { id = invoice.InvoiceId }, invoice);
        }

        // PUT: api/Invoice/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, [FromBody] Invoice invoice)
        {
            if (id != invoice.InvoiceId)
            {
                return BadRequest(new { Message = "Invoice ID mismatch." });
            }

            _context.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
                {
                    return NotFound(new { Message = $"Invoice with id {id} not found." });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Invoice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound(new { Message = $"Invoice with id {id} not found." });
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Utility method to check if an invoice exists
        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.InvoiceId == id);
        }
    }
}
