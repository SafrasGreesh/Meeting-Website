using System;
using System.Collections.Generic;
using System.Text;
using MeetingWebsite.Entity;
using MeetingWebsite.ViewModels;

namespace MeetingWebsite.Services.HubService
{
    public interface IUserService
    {
        void UpdateProfile(ProfileViewModel model);

        OponentViewModel GetOponentProfile(string id);

        IEnumerable<UserViewModel> FindUserByMatch(string match, string curentUser);

        ProfileViewModel GetUserProfile(string userId);

        void AddAvatar(string avatarId, string userId);

        bool isEmailUniq(string email);

        bool isUsernameUniq(string userName);

        Users GetUserByEmail(string email);

        Users CreateUser(string username, string email, string password);

        void AddUser(Users newUser);

        string GetUserNameById(string id);

        string GetUserIdByName(string name);

        string GetOponentIdByTheadId(string senderId, string threadId);

        ICollection<Users> GetUsers();
        


    }
}
