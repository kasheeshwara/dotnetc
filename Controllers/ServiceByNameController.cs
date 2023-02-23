using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceProject.Controllers
{
    [Route("api/[controller]")]
   
    [ApiController]
    public class ServiceByNameController : ControllerBase
    {
        private readonly ServiceContext _context;

        public ServiceByNameController(ServiceContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<ServiceRequest>>> GetServiceRequests(int userId)
        {
            var serviceRequest = await _context.ServiceRequests.Where(u => u.UserId == userId).ToListAsync();

            if (serviceRequest == null)
            {
                return NotFound();
            }

            return serviceRequest;
        }

    }
}
