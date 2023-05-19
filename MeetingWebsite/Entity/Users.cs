using Microsoft.AspNetCore.Identity;

namespace MeetingWebsite.Entity
{
    public class Users : BaseEntity
    {
        public string Password { get; set; }

        //public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Mail { get; set; }
        public string City { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }

        public ICollection<Chat> Chats {get;} = new List<Chat>();

        public ICollection<Dislikes> Dislikes {get;} = new List<Dislikes>();

        public ICollection<Likes> Likes {get;} = new List<Likes>();

        public ICollection<Events> Events {get;} = new List<Events>();
    }
}
