using System;
using System.Collections.Generic;
using ems_backend.Models;

namespace ems_backend.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? City { get; set; }
        public string? Designation { get; set; }
        public string? MailAddress { get; set; } 
        public string? PasswordHash { get; set; } = "";
        public string? PasswordSalt { get; set; } = "";
        public Role? Role { get; set; } = Entities.Role.user;
        public int EnrollmentId { get; set; }
        public int CategoryId { get; set; }

        public virtual ICollection<Enrollment>? Enrollments { get; set; }
        public virtual ICollection<Category>? Categories { get; set; }
    }
}
