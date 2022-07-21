using System.Text.Json.Serialization;
using ems_backend.Entities;
using Newtonsoft.Json.Converters;

namespace ems_backend.Models
{
    public class UserRequestModel
    {
        public string Username { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Designation { get; set; } = null!;
        public string MailAddress { get; set; } = null!;
        public Role? Role { get; set; }
    }
}