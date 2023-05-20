namespace MeetingWebsite.Entity
{
    public class Matches : BaseEntity
    {
        public int? Id { get; set; }

        public int? UserId1 { get; set; }
        public int? UserId2 { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
