namespace MeetingWebsite.Entity
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Users Admin { get; set; }
        public ICollection<Messages> Messages { get; set; }
    }
}
