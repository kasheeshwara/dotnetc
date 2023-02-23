using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ServiceContext _context;
        public ReportsController (ServiceContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
       public async Task<ActionResult<List<ServiceRequestReport>>> GetServiceRequestReports(int id)
        {
            var serviceRequests = await _context.ServiceRequestReports.Where(u => u.SerReqId == id).ToListAsync();

            if (serviceRequests == null)
            {
                return NotFound();
            }

            return serviceRequests;
        } 
    }
}
