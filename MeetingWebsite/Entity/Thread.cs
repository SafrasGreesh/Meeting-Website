using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MeetingWebsite.Entity;

namespace MeetingWebsite.Entity
{
    public class Thread : BaseEntity
    {
        public Thread()
        {
            CreatedOn = DateTime.Now;
            Messages = new HashSet<Message>();
        }

        [Required]
        [ForeignKey("OwnerId")]
        public string OwnerId { get; set; }
        public Users Owner { get; set; }

        public string OponentId { get; set; }


        public ICollection<Message> Messages { get; set; }
    }
}
