namespace MeetingWebsite.Entity
{
    public class Likes
    {
        public int? Id { get; set; }
            
        public int? UserId { get; set; }
        public int? LikeUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
