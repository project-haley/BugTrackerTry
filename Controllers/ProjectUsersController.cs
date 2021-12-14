using BugTrackerTry.Data;
using BugTrackerTry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerTry.Controllers
{
    [Authorize(Roles="Administrator")]
    public class ProjectUsersController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ProjectUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProjectUsersController(RoleManager<IdentityRole> roleManager, ApplicationDbContext context, UserManager<ProjectUser> userManager)
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        //GET
        [HttpGet]
        public async Task<IActionResult> ManageUsers()
        {
            var applicationDbContext = await _context.Users.ToListAsync();
            return View(applicationDbContext.OrderByDescending(m => m.Id));
        }

        //POST
        //[HttpPost]
        //public async Task<IActionResult> ManageUsers(ProjectUser model, string role)
        //{

        //    await _userManager.AddToRoleAsync(model, role);
        //    return RedirectToAction("Index");
        //}

        //GET
        [HttpGet]
        public async Task<IActionResult> UserDetails(string id)
        {
            var projectUser = await _context.Users
                .Include(u => u.Projects)
                .Include(u => u.Tickets)
                .Include(u => u.TicketComments)
                .FirstOrDefaultAsync(u => u.Id == id);
            return View(projectUser);
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> UserDetails([Bind("Id, FirstName, LastName, DisplayName, TwitterUrl, LinkedInUrl")] ProjectUser projectUser, IEnumerable<string> roleNames)
        {
            //Updated user properties
            //Using for role management
            //Updating db record with form values after
            var updatedUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == projectUser.Id);

            //Change user role(s)
            var oldUserRoles = await _userManager.GetRolesAsync(updatedUser);
            await _userManager.RemoveFromRolesAsync(updatedUser, oldUserRoles);
            await _userManager.AddToRolesAsync(updatedUser, roleNames);

            var roleCheck = await _userManager.GetRolesAsync(updatedUser);

            //Pass form model values to user record
            updatedUser.FirstName = projectUser.FirstName;
            updatedUser.LastName = projectUser.LastName;
            updatedUser.DisplayName = projectUser.DisplayName;
            updatedUser.TwitterUrl = projectUser.TwitterUrl;
            updatedUser.LinkedInUrl = projectUser.LinkedInUrl;

            await _context.SaveChangesAsync();

            


            return RedirectToAction("ManageUsers");
        }
    }
}
