using System;
using System.Collections.Generic;

namespace SWP391.Models
{
    public partial class Contract
    {
        public Contract()
        {
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int TenantId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MonthlyRent { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Property Property { get; set; } = null!;
        public virtual User Tenant { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
