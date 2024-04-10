using System;
using System.Collections.Generic;

namespace SWP391.Models
{
    public partial class User
    {
        public User()
        {
            Bookings = new HashSet<Booking>();
            Contracts = new HashSet<Contract>();
            FavoriteProperties = new HashSet<FavoriteProperty>();
            Properties = new HashSet<Property>();
            Reviews = new HashSet<Review>();
            SupportTickets = new HashSet<SupportTicket>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; } = null;
        public string Role { get; set; } = null!;
        public string? VerificationToken { get; set; }
        public Boolean? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<FavoriteProperty> FavoriteProperties { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<SupportTicket> SupportTickets { get; set; }
    }
}
