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
                .ForMember(dst => dst.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dst => dst.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dst => dst.Photo, opt => opt.MapFrom(src => src.Photo))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                ;
            
       
      
       


        //Password = user.Password;
        //Id = user.Id;
        //Name = user.Name;
        //Mail = user.Mail;
        //Token = token;

        CreateMap<Users, AuthenticateResponse>()
                
                .ForMember(dst => dst.Mail, opt => opt.MapFrom(src => src.Mail))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dst => dst.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dst => dst.Photo, opt => opt.MapFrom(src => src.Photo))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.Token, opt => opt.Ignore())
                ;
        }
    }
}
