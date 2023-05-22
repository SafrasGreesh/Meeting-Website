using AutoMapper;
using MeetingWebsite.Entity;
using MeetingWebsite.ViewModel;

namespace MeetingWebsite.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Users, UserViewModel>()
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(x => x.UserName));

            CreateMap<UserViewModel, Users>();
        }
    }
}
