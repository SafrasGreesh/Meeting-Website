// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using MeetingWebsite.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MeetingWebsite.Pages
{
    public class IndexModel1 : PageModel
    {
        private readonly UserManager<Users> _userManager;
        private readonly ILogger<IndexModel1> _logger;
        private readonly IWebHostEnvironment _env;

        public IndexModel1(UserManager<Users> userManager, ILogger<IndexModel1> logger, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _logger = logger;
            _env = env;
        }

        public string Username { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string UploadAvatarErrorMessage { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        [BindProperty]
        public IFormFile AvatarFile { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(Users user)
        {
            var userName = user.UserName; // Assuming Users has a UserName property

            Username = userName;
            FullName = user.Name;
            Avatar = user.Avatar;

        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }


            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAvatarAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            var deleted = DeleteAvatar(user.Avatar);
            if (deleted)
            {
                user.Avatar = null;
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to update avatar.";
                    return RedirectToPage();
                }
            }

            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        private bool DeleteAvatar(string fileName)
        {
            try
            {
                var folderPath = Path.Combine(_env.WebRootPath, "avatars");
                var filePath = Path.Combine(folderPath, Path.GetFileName(fileName));
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting avatar: {fileName}.", ex.Message);
            }

            return false;
        }
    }

}
