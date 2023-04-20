using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeetingWebsite.Pages
{
    public class ChoisePeopleModel : PageModel
    {
        private readonly ILogger<ChoisePeopleModel> _logger;

        public ChoisePeopleModel(ILogger<ChoisePeopleModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}