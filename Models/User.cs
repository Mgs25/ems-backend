using System;
using System.Collections.Generic;

namespace ems_backend.Models
{
    public partial class User
    {
        public User()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string MailAddress { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;

        public virtual ICollection<Enrollment>? Enrollments { get; set; }
    }
}
