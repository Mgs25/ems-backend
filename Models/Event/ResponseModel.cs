namespace ems_backend.Models
{
    public class EventResponseModel : EventRequestModel
    {
        public int EventId { get; set; }
        public int ViewCount { get; set; }
        public int ParticipantsCount { get; set; }
    }
}