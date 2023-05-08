using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeetingWebsite.Pages
{
    public class EventModel : PageModel
    {
        private readonly ILogger<EventModel> _logger;

        public EventModel(ILogger<EventModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}