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
        Users GetById(int id);
    }
}
