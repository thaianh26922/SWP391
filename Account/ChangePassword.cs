using System.ComponentModel.DataAnnotations;

namespace SWP391.Account
{
    public class ChangePassword
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string OldPassword { get; set; }
        
        public string NewPassword { get; set; }
       
        public string ConfirmNewPassword { get; set; }
    }
}
