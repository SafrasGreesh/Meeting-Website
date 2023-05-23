using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingWebsite.Interfaces
{
    public interface IAuditable
    {
        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
