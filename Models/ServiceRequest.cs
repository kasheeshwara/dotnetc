using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceProject.Models
{
    public partial class ServiceRequest
    {
        public ServiceRequest()
        {
            ServiceRequestReports = new HashSet<ServiceRequestReport>();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public DateTime ReqDate { get; set; }
        public string Problem { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ServiceRequestReport> ServiceRequestReports { get; set; }
    }
}
