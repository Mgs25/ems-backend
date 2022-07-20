using System;
using System.Collections.Generic;

namespace ems_backend.Entities
{
    public partial class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }

        public virtual Event? Event { get; set; } = null;
        public virtual User? User { get; set; } = null;
    }
}
