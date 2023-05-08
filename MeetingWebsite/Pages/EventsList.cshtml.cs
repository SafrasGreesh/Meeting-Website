using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeetingWebsite.Pages
{
    public class EventsListModel : PageModel
    {
        private readonly ILogger<EventsListModel> _logger;

        public EventsListModel(ILogger<EventsListModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}