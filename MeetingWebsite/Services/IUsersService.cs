using MeetingWebsite.Entity;
using MeetingWebsite.Models;

using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingWebsite.Entity;
using MeetingWebsite.Models;

namespace MeetingWebsite.Services
{
    public interface IUsersService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<AuthenticateResponse> Register(UserModel userModel);
        IEnumerable<Users> GetAll();
        Users GetById(string id);
        Task<AuthenticateResponse> UpdateInformation(UserModel userModel, string? id);
        IEnumerable<Users> Swipe(int id_y);
        Task<bool> UpdateOptions(Options optModel, string? id);
        Options GetOptionsById(int id);
        Task<bool> Like(int? id1, int? id2, Boolean like_u);
        IEnumerable<int> Matches(int id_y);

    }
}
