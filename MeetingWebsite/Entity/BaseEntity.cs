using MeetingWebsite.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MeetingWebsite.Entity
{
    public class BaseEntity : IAuditable, IDeletable
    {
        [Key]
        public string Id { get; set; }
        public bool isDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
    }
}
