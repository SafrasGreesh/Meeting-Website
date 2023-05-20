using MeetingWebsite.Entity;
using MeetingWebsite.Models;

using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingWebsite.Entity;
using MeetingWebsite.Models;

namespace MeetingWebsite.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<AuthenticateResponse> Register(UserModel userModel);
        IEnumerable<Users> GetAll();
        IEnumerable<Events> GetAllEvents();
        Users GetById(int? id);
        Events GetEventById(int? id);
        Task<int> GetMaxEventId();
        Task<int> AddEvent(Events eventModel, int id_Ev);
    }
}
