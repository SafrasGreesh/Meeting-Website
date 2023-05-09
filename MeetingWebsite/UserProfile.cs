using AutoMapper;
using MeetingWebsite.Entity;
using MeetingWebsite.Models;




namespace MeetingWebsite
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, Users>()
                .ForMember(dst => dst.Mail, opt => opt.MapFrom(src => src.Mail))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.Password))
                //.ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
                //.ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.LastName))
                //.ForMember(dst => dst.Patronymic, opt => opt.MapFrom(src => src.Patronymic))
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                ;

            //Password = user.Password;
            //Id = user.Id;
            //Name = user.Name;
            //Mail = user.Mail;
            //Token = token;

            CreateMap<Users, AuthenticateResponse>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Mail, opt => opt.MapFrom(src => src.Mail))
                //.ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.Email))
                //.ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.Email))
                //.ForMember(dst => dst.Patronymic, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Token, opt => opt.Ignore())
                ;
        }
    }
}
