using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceProject.Models
{
    public partial class Product
    {
        public Product()
        {
            ServiceRequests = new HashSet<ServiceRequest>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal Cost { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; }
    }
}
