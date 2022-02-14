using BugTrackerTry.Data;
using BugTrackerTry.Services;
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
        private readonly SearchService _searchService;

        public SearchController(ApplicationDbContext context, SearchService searchService)
        {
            _context = context;
            _searchService = searchService;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["SearchString"] = searchString;

            var projects = _searchService.SearchProjects(searchString);

            return View(projects);

        }
    }
}
