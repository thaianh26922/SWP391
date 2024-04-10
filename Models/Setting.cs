using System;
using System.Collections.Generic;

namespace SWP391.Models
{
    public partial class Setting
    {
        public int Id { get; set; }
        public string SettingName { get; set; } = null!;
        public string SettingValue { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
