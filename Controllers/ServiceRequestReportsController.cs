using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceProject.Models;

namespace ServiceProject.Controllers
{
    [Route("api/[controller]")]
   
    [ApiController]
    public class ServiceRequestReportsController : ControllerBase
    {
        private readonly ServiceContext _context;

        public ServiceRequestReportsController(ServiceContext context)
        {
            _context = context;
        }

        // GET: api/ServiceRequestReports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceRequestReport>>> GetServiceRequestReports()
        {
            return await _context.ServiceRequestReports.ToListAsync();
        }

        // GET: api/ServiceRequestReports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRequestReport>> GetServiceRequestReport(int id)
        {
            var serviceRequestReport = await _context.ServiceRequestReports.FindAsync(id);

            if (serviceRequestReport == null)
            {
                return NotFound();
            }

            return serviceRequestReport;
        }

        // PUT: api/ServiceRequestReports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceRequestReport(int id, ServiceRequestReport serviceRequestReport)
        {
            if (id != serviceRequestReport.Id)
            {
                return BadRequest();
            }

            _context.Entry(serviceRequestReport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceRequestReportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ServiceRequestReports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        
        public async Task<ActionResult<ServiceRequestReport>> PostServiceRequestReport(ServiceRequestReport serviceRequestReport)
        {
            _context.ServiceRequestReports.Add(serviceRequestReport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServiceRequestReport", new { id = serviceRequestReport.Id }, serviceRequestReport);
        }

        // DELETE: api/ServiceRequestReports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceRequestReport(int id)
        {
            var serviceRequestReport = await _context.ServiceRequestReports.FindAsync(id);
            if (serviceRequestReport == null)
            {
                return NotFound();
            }

            _context.ServiceRequestReports.Remove(serviceRequestReport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceRequestReportExists(int id)
        {
            return _context.ServiceRequestReports.Any(e => e.Id == id);
        }
    }
}
