

using MeetingWebsite.ViewModels;

namespace MeetingWebsite.Services.HubService
{
    public interface IAuthService
    {
        AuthData GetToken(string id);

        string HashPassword(string password);

        bool VerifyPassword(string actualPassword, string hashedPassword);

    }
}