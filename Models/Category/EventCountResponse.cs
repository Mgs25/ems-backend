namespace ems_backend.Models
{
    public class EventCountResponseModel
    {
        public EventCountResponseModel(int value, string name)
        {
            this.value = value;
            this.name = name;
        }
        public int value { get; set; }
        public string? name { get; set; }
    }
}