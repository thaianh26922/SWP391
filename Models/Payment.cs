using System;
using System.Collections.Generic;

namespace SWP391.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Contract Contract { get; set; } = null!;
    }
}
