using System;
using System.Collections.Generic;

namespace SWP391.Models
{
    public partial class FavoriteProperty
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Property Property { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
