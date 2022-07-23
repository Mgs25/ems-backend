using System.ComponentModel.DataAnnotations;

namespace ems_backend.Models
{
    public class LoginRequestModel
    {
        [Required]
        public string UsernameOrMailAddress { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}