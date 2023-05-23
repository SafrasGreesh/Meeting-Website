using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeetingWebsite.Interfaces;
using MeetingWebsite.Services;
using MeetingWebsite.Services.HubService;


namespace MeetingWebsite.Helpers
{
    public class Validator : IValidator
    {
        private readonly IThreadService threadService;
        private readonly ApplicationContext ctx;

        public Validator(IThreadService threadService, ApplicationContext ctx)
        {
            this.threadService = threadService;
            this.ctx = ctx;
        }

        public bool DoesThreadExist(string id)
        {
            return ctx.Thread.Any(t => t.Id == id);
        }

        public bool DoesUserBelongToCurentThread(string threadId, string userId)
        {
            var thread = ctx.Thread.FirstOrDefault(t => t.Id == threadId);
            if (thread.OwnerId == userId || thread.OponentId == userId)
            {
                return true;
            }

            return false;
        }
    }
}
