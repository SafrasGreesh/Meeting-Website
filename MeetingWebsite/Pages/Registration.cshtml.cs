﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeetingWebsite.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly ILogger<RegistrationModel> _logger;

        public RegistrationModel(ILogger<RegistrationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}