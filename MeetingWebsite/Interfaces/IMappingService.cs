using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeetingWebsite.Entity;
using MeetingWebsite.ViewModels;
using Thread = MeetingWebsite.Entity.Thread;


namespace MeetingWebsite.Interfaces
{
    public interface IMappingService
    {
        MessageViewModel MapMessageModelToMessageViewModel(Message model);

        Message MapMessageViewModelToMessageModel(MessageViewModel model);

        IEnumerable<MessageViewModel> MapMessageModelCollectionToMessageViewModelCollection(IEnumerable<Message> collection);

        ThreadViewModel MapThreadModelToThreadViewModel(Thread model);

        Thread MapThreadViewModelToThreadModel(ThreadViewModel model);

        UserViewModel MapUserModelToUserViewModel(Users model);

        ProfileViewModel MapUserModelRoProfileViewModel(Users model);
    }
}
