using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeetingWebsite.Pages
{
    public class UserInformationModel : PageModel
    {
        private readonly ILogger<UserInformationModel> _logger;

        public UserInformationModel(ILogger<UserInformationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}