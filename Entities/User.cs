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
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[]? PasswordSalt { get; set; } = new byte[32];
        public string? VerificationToken { get; set; }
        public DateTime VerifiedAt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshCreated { get; set; }
        public DateTime RefreshExpires { get; set; }
        public string? Otp { get; set; }
        public DateTime OtpExpires { get; set; }
        
        public Role? Role { get; set; } = Entities.Role.user;
        public int EnrollmentId { get; set; }
        public int CategoryId { get; set; }

        public virtual ICollection<Enrollment>? Enrollments { get; set; }
        public virtual ICollection<Category>? Categories { get; set; }
    }
}
