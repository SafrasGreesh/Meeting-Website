using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeetingWebsite.Pages
{
    public class ChatUsersModel : PageModel
    {
        private readonly ILogger<ChatUsersModel> _logger;

        public ChatUsersModel(ILogger<ChatUsersModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}