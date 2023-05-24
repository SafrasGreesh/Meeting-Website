using MeetingWebsite.Entity;

namespace MeetingWebsite.Models
{
    public class AuthenticateResponse
    {
        public string? Password { get; set; }

        public int? Id { get; set; }
        public string? Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Mail { get; set; }
        public string? City { get; set; }
        public string? Photo { get; set; }
        public string? Description { get; set; }
        public string? Gender { get; set; }
        public string? Token { get; set; }

        public AuthenticateResponse(Users user, string token)
        {
            Password = user.Password;
            Id = user.Id;
            Name = user.Name;
            Mail = user.Mail;
            Token = token;
            Gender = user.Gender;
            Description = user.Description;
            Photo = user.Photo;
            BirthDate = user.BirthDate;
            City = user.City;

        }
    }
}
