using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingWebsite.Interfaces
{
    public interface IDeletable
    {
        bool isDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}
