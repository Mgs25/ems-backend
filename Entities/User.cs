using System;
using System.Collections.Generic;
using ems_backend.Models;

namespace ems_backend.Entities
{
    public partial class User
    {
        // public User()
        // {
        //     Enrollments = new HashSet<Enrollment>();
        // }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string MailAddress { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;

        public virtual ICollection<Enrollment>? Enrollments { get; set; }
    }
}
