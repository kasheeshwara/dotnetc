using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceProject.Models
{
    public partial class ServiceRequestReport
    {
        public int Id { get; set; }
        public int SerReqId { get; set; }
        public DateTime ReportDate { get; set; }
        public string ServiceType { get; set; }
        public string ActionTaken { get; set; }
        public string DiagnosisDetails { get; set; }
        public string IsPaid { get; set; }
        public decimal VisitFees { get; set; }
        public string RepairDetails { get; set; }

        public virtual ServiceRequest SerReq { get; set; }
    }
}
