using System;
using System.Collections.Generic;

namespace SWP391.Models
{
    public partial class Property
    {
        public Property()
        {
            Bookings = new HashSet<Booking>();
            Contracts = new HashSet<Contract>();
            FavoriteProperties = new HashSet<FavoriteProperty>();
            PropertyImages = new HashSet<PropertyImage>();
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string Address { get; set; } = null!;
        public decimal? Area { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int OwnerId { get; set; }

        public virtual User Owner { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<FavoriteProperty> FavoriteProperties { get; set; }
        public virtual ICollection<PropertyImage> PropertyImages { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
