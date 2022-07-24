using System.ComponentModel.DataAnnotations;

namespace ems_backend.Models
{
    public class RegisterRequestModel
    {
        [Required]
        public string Username { get ; set; } = string.Empty;
        [Required, EmailAddress]
        public string MailAddress { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Designation { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}