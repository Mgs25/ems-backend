using System;
using System.Collections.Generic;
using ems_backend.Models;

namespace ems_backend.Entities
{
    public partial class Event
    {
        // public Event()
        // {
        //     Enrollments = new HashSet<Enrollment>();
        // }

        public int EventId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string? Cover { get; set; }
        public string Venue { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public int? ParticipationLimit { get; set; }

        public virtual ICollection<Enrollment>? Enrollments { get; set; }
    }
}
