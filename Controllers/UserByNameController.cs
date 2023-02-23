using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceProject.Models;
using System.Threading.Tasks;

namespace ServiceProject.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    public class UserByNameController : ControllerBase
    {

        private readonly ServiceContext _context;
        private readonly ServiceContext _authContext;
        public UserByNameController(ServiceContext context, ServiceContext authContext)
        {
            _context = context;
            _authContext = authContext;

        }

        [HttpGet("{name}")]
        public async Task<ActionResult<User>> GetUser(string name)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.Name==name);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
    }
}
