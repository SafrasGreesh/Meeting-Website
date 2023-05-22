namespace MeetingWebsite.Entity
{
    public class Dislikes
    {
        public int? Id { get; set; }

        public int? UserId { get; set; }
        public int? DislikesUserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Users Users { get; set; } = null!;
    }
}
