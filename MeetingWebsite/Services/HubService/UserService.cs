using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeetingWebsite.Entity;
using MeetingWebsite.Interfaces;
using MeetingWebsite.ViewModels;
using WebChat.Hubs.Interfaces;
using Thread = MeetingWebsite.Entity.Thread;

namespace MeetingWebsite.Services.HubService
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext ctx;
        private readonly IAuthService authService;
        private readonly IThreadService threadService;
        private readonly IMappingService mappingService;
        private readonly IConnectionMapping<string> connectionMapping;

        public UserService(
            ApplicationContext ctx, 
            IAuthService authService, 
            IThreadService threadService, 
            IMappingService mappingService, 
            IConnectionMapping<string> connectionMapping)
        {
            this.ctx = ctx ?? throw new ArgumentNullException("Context can not be null");
            this.authService = authService ?? throw new ArgumentNullException("Authorization service can not be null");  
            this.threadService = threadService ?? throw new ArgumentNullException("Thread service service can not be null");
            this.mappingService = mappingService ?? throw new ArgumentNullException("Mapping service service can not be null");
            this.connectionMapping = connectionMapping ?? throw new ArgumentNullException(nameof(connectionMapping));
        }

        public void UpdateProfile(ProfileViewModel model)
        {
            var entity = this.ctx.Users.FirstOrDefault(u => u.Id == model.Id);
            entity.Mail = model.Email;
            entity.Username = model.Username;
            ctx.Users.Update(entity);
            ctx.SaveChanges();
        }

        public IEnumerable<UserViewModel> FindUserByMatch(string match, string curentUser)
        {
            if (string.IsNullOrEmpty(curentUser)) throw new ArgumentNullException("Current user Id can not be null");

            var queryResult = ctx.Users.Where(u => u.Username.IndexOf(match) > -1 && u.Id != curentUser);
            var searchResult = new List<UserViewModel>();
            foreach (var user in queryResult)
            {
                //Check is current user has any connection to provide online/offline status
                List<string> userConnections = connectionMapping.GetConnections(user.Id).ToList();

                var userVm = mappingService.MapUserModelToUserViewModel(user);
                userVm.IsOnline = userConnections.Count == 0 ? false : true;
                searchResult.Add(userVm);
            }
            return searchResult;
        }

        public void AddAvatar(string avatarId, string userId)
        {
            var user = ctx.Users.FirstOrDefault(u => u.Id == userId);
            user.AvatarFileName = avatarId;
            ctx.Users.Update(user);
            ctx.SaveChanges();
        }

        public void AddUser(Users newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException("User Entity can not be null");
            }

            newUser.CreatedOn = DateTime.Now;

            ctx.Users.Add(newUser);
            ctx.SaveChanges();
        }

        public Users CreateUser(string username, string email, string password)
        {

            var newUser = new Users()
            {
                Id = Guid.NewGuid().ToString(),
                Username = username,
                Mail = email,
                Password = authService.HashPassword(password)
            };

            return newUser;
        }

        public string GetOponentIdByTheadId(string senderId, string threadId)
        {
            //Get thread from thread service
            Thread currentThread = this.threadService.GetThreadById(threadId);
            if (currentThread.OwnerId == senderId)
            {
                return currentThread.OponentId;
            }
            return currentThread.OwnerId;
            throw new NotImplementedException();
        }

        public Users GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("Email can not be null or empty");
            }

            return ctx.Users.FirstOrDefault(u => u.Mail == email);
        }

        public string GetUserIdByName(string name)
        {
            return ctx.Users.FirstOrDefault(u => u.Username == name).Id;
        }

        public string GetUserNameById(string id)
        {
            return ctx.Users.FirstOrDefault(u => u.Id == id).Username;
        }

        public ProfileViewModel GetUserProfile(string userId)
        {
            var model = ctx.Users.FirstOrDefault(u => u.Id == userId);
            var viewModel = this.mappingService.MapUserModelRoProfileViewModel(model);
            viewModel.Username = GetUserNameById(userId);

            return viewModel;

        }
        //TODO: Check opponent's status
        public OponentViewModel GetOponentProfile(string id)
        {
            var userConnections = connectionMapping.GetConnections(id);

            var profile = (from u in ctx.Users
                           where u.Id == id
                           select new OponentViewModel
                           {
                               Id = u.Id, Username = u.Username,
                               AvatarFileName = u.AvatarFileName,
                               IsOnline = userConnections.Count() > 0 ? true : false
                           }).FirstOrDefault();
            return profile;
        }

        public ICollection<Users> GetUsers()
        {
            return ctx.Users.ToList();
        }

        public bool isEmailUniq(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("Email can not be null or empty");
            }
            var user = ctx.Users.FirstOrDefault(u => u.Mail == email);

            if (user != null)
            {
                return false;
            }

            return true;
        }

        public bool isUsernameUniq(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("Username can not be null or empty");
            }
            var user = ctx.Users.FirstOrDefault(u => u.Username == userName);

            if (user != null)
            {
                return false;
            }

            return true;
        }


    }
}
