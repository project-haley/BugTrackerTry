using BugTrackerTry.Data;
using BugTrackerTry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerTry.Services
{
    public class SearchService
    {
        private readonly ApplicationDbContext _context;

        public SearchService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Project> SearchProjects(string searchString)
        {
            var projects = from t in _context.Projects
                           select t;
            searchString = searchString.ToLower();

            if (!String.IsNullOrEmpty(searchString))
            {
                projects = projects.Where(p =>
                p.Name.ToLower().Contains(searchString) ||
                p.Description.ToLower().Contains(searchString));
            }

            projects = projects.OrderByDescending(p => p.Created);

            return projects;
        }
    }
}
