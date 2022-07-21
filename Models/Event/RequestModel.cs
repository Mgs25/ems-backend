using System.Text.Json.Serialization;
using ems_backend.Entities;

namespace ems_backend.Models
{
    public class EventRequestModel
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        // public string Category { get; set; } = null!;
        public string Cover { get; set; } = null!;
        public string Venue { get; set; } = null!;
        public string Organizer { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public int? ParticipationLimit { get; set; }
        public int? CategoryId { get; set; } = null!;
    }
}