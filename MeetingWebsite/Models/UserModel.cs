using System.Text.Json.Serialization;

namespace MeetingWebsite.Models
{
    public class UserModel
    {
        //public int Id { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Patronymic { get; set; }
        //public string Name { get; set; }
        //public string Email { get; set; }
        //public string Password { get; set; }
        

        public string Password { get; set; }

        public string? Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Mail { get; set; }
        public string City { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
    }
}
