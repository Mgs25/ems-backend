using System;
using System.Collections.Generic;
using ems_backend.Models;

namespace ems_backend.Entities
{
    public class Event
    {
        public int EventId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Cover { get; set; }
        public string? Venue { get; set; }
        public string? Organizer { get; set; }
        public DateTime Date { get; set; } = new DateTime();
        public int ParticipationLimit { get; set; }
        public int ParticipantsCount { get; set; } = 0;
        public int ViewCount { get; set; } = 1;
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
