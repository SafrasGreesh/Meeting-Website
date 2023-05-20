namespace MeetingWebsite.Entity
{
    public class Events : BaseEntity
    {

        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
