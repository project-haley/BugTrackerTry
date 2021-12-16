using BugTrackerTry.Data;
using BugTrackerTry.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerTry.Controllers
{
    public class TicketCommentsController : Controller
    {
        private readonly UserManager<ProjectUser> _userManager;
        private readonly ApplicationDbContext _context;

        public TicketCommentsController(UserManager<ProjectUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // POST: TicketCommentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketId, CommentBody")] TicketComment ticketComment)
        {
            if (ModelState.IsValid)
            {
                ticketComment.Created = DateTime.Now;
                ticketComment.Updated = DateTime.Now;
                ticketComment.ProjectUserId = _userManager.GetUserId(User);

                _context.Add(ticketComment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction($"Details", "Tickets", new { id = ticketComment.TicketId });
            
        }

        // GET: TicketCommentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TicketCommentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
