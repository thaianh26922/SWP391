using System;
using System.Collections.Generic;

namespace SWP391.Models
{
    public partial class PropertyImage
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Property Property { get; set; } = null!;
    }
}
