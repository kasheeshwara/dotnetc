using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceProject.Models
{
    public partial class User
    {
        public User()
        {
            ServiceRequests = new HashSet<ServiceRequest>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public decimal Mobile { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }

        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; }
    }
}
