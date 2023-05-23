using MeetingWebsite.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MeetingWebsite.Entity
{
    public class Users:BaseEntity, IAuditable, IDeletable
    {
        public string Password { get; set; }

        //public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string City { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public Users()
        {
            Messages = new HashSet<Message>();
        }
        [MaxLength(60)]
        public string? Username { get; set; }

        [Required]
        [MaxLength(60)]
        public string Mail { get; set; }

        public string? AvatarFileName { get; set; }
        public ICollection<Message> Messages { get; set; }

        public ICollection<Thread> Threads { get; set; }
    }
}
