﻿using System.Threading.Tasks;
using BugTrackerTry.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BugTrackerTry.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<ProjectUser> _userManager;
        private readonly ILogger<PersonalDataModel> _logger;

        public PersonalDataModel(
            UserManager<ProjectUser> userManager,
            ILogger<PersonalDataModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }
    }
}