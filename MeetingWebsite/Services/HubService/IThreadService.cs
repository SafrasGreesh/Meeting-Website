﻿using System;
using System.Collections.Generic;
using System.Text;
using MeetingWebsite.ViewModels;

namespace MeetingWebsite.Services.HubService
{
    public interface IThreadService
    {
        Dictionary<DateTime, List<MessageViewModel>> SearchForMessages(string threadId, string term);

        ICollection<Thread> GetUserThreads(string userId);

        void AddThread(ThreadViewModel thread);

        Thread GetThreadById(string threadId);

        ThreadViewModel CreateThreadViewModel(string ownerId, string oponentId);
        //Testing
        List<MessageViewModel> GetThreadMessages(string threadId);

        string GetLastMessageForThread(string threadId);

        LastMessageViewModel GetThreadLastMessage(string threadId);
    }
}
