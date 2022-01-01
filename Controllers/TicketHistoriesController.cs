using BugTrackerTry.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerTry.Controllers
{
    public class TicketHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TicketHistoriesController/Details/5
        public async Task<IActionResult> Details(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var ticketHistory = await _context.TicketHistories
                .Include(t => t.Ticket)
                .Include(t => t.TicketComments)
                .Include(t => t.TicketAttachments)
                .Include(t => t.TicketSnapshots)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticketHistory == null)
            {
                return NotFound();
            }


            return View(ticketHistory);
        }

        // POST: TicketHistoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: TicketHistoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
