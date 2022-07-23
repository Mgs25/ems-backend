using System.ComponentModel.DataAnnotations;

namespace ems_backend.Models
{
    public class ResetPasswordModel
    {
        // public string Token { get; set; } = string.Empty;
        [Required]
        public string Otp { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}