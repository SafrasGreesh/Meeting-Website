using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeetingWebsite.Pages
{
    public class UserSettingsModel : PageModel
    {
        private readonly ILogger<UserSettingsModel> _logger;

        public UserSettingsModel(ILogger<UserSettingsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}