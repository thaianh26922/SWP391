using System;
using System.Collections.Generic;

namespace SWP391.Models
{
    public partial class UserDto
    {
       
        public string userName { get; set; } = null!;
        public string password { get; set; } = null!;

        public string email { get; set; } = null!;

    }
}
