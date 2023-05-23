using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingWebsite.Interfaces
{
    public interface IValidator
    {
        bool DoesThreadExist(string id);

        bool DoesUserBelongToCurentThread(string threadId, string userId);
    }
}
