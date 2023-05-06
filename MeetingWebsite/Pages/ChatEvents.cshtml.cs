using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeetingWebsite.Pages
{
    public class ChatEventsModel : PageModel
    {
        private readonly ILogger<ChatEventsModel> _logger;

        public ChatEventsModel(ILogger<ChatEventsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}