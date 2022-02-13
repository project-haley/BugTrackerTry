using BugTrackerTry.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerTry.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var projects = from m in _context.Projects
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                projects = projects.Where(s => s.Name!.Contains(searchString));
            }

            return View(await projects.ToListAsync());

        }
    }
}
