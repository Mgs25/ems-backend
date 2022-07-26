using System.Text.Json.Serialization;
using ems_backend.Entities;

namespace ems_backend.Models
{
    public class BarResponseModel
    {
        public List<string>? EventList { get; set; }
        public List<int>? ParticipantsCount { get; set; }
        public List<int>? ParticipantsLimit { get; set; }
    }
}