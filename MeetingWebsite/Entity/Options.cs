using Microsoft.AspNetCore.Identity;

namespace MeetingWebsite.Entity
{
    public class Options:BaseEntity
    {
        public int? Id { get; set; }
        public int AgeMin { get; set; }
        public int AgeMax { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }

    }
}
