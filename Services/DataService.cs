using BugTrackerTry.Data;
using BugTrackerTry.Enums;
using BugTrackerTry.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerTry.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ProjectUser> _userManager;

        // constructor injection
        public DataService(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<ProjectUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ManageDataAsync()
        {
            // Create db from the Migrations
            // Ensures db is created in event it doesn't exist
            await _dbContext.Database.MigrateAsync();

            // 1: Seed roles into the system
            await SeedRolesAsync();

            // 2: Seed a few users into the system
            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            // If roles already exist, do nothing.
            if (_dbContext.Roles.Any())
            {
                return;
            }

            // Otherwise, create a few roles
            foreach (var role in Enum.GetNames(typeof(ProjectRoles)))
            {
                // use the role manager to create roles
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedUsersAsync()
        {
            // If users exist, do nothing.
            if (_dbContext.Users.Any())
            {
                return;
            }

            // Otherwise, create them
            var adminUser = new ProjectUser()
            {
                Email = "seeduser1@mailinator.com",
                UserName = "seeduser1@mailinator.com",
                FirstName = "John",
                LastName = "Doe",
                DisplayName = "seeduser1",
                EmailConfirmed = true
            };
            var developerUser = new ProjectUser()
            {
                Email = "seeduser2@mailinator.com",
                UserName = "seeduser2@mailinator.com",
                FirstName = "Jane",
                LastName = "Doe",
                DisplayName = "seeduser2",
                EmailConfirmed = true
            };

            // Then, use _userManager to create said user as defined by adminUser
            await _userManager.CreateAsync(adminUser, "Abc123!");

            // Then, add this to db
            await _userManager.AddToRoleAsync(adminUser, ProjectRoles.Administrator.ToString());

            // another user
            await _userManager.CreateAsync(developerUser, "Abc123!");

            // another user
            await _userManager.AddToRoleAsync(developerUser, ProjectRoles.Developer.ToString());
        }
    }
}
