using Microsoft.AspNetCore.Identity;

namespace MeetingWebsite.Entity
{
    public class Users
    {
        private int Password { get; set; }

        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Mail { get; set; }
        public string City { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
    }
}
