using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeetingWebsite.Pages
{
    public class LikeModel : PageModel
    {
        private readonly ILogger<LikeModel> _logger;

        public LikeModel(ILogger<LikeModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}