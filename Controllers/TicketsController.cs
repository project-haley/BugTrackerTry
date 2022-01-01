using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BugTrackerTry.Data;
using BugTrackerTry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using BugTrackerTry.Services;

namespace BugTrackerTry.Controllers
{
    [Authorize(Roles = "Administrator, Developer, ProjectManager, Submitter")]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ProjectUser> _userManager;
        private readonly IImageService _imageService;

        public TicketsController(ApplicationDbContext context, UserManager<ProjectUser> userManager, IImageService imageService)
        {
            _context = context;
            _userManager = userManager;
            _imageService = imageService;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tickets
                .Include(t => t.Project)
                .Include(t => t.TicketHistory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Project)
                .Include(t => t.TicketAttachments)
                .Include(t => t.TicketComments)
                .Include(t => t.TicketHistory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");

            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Body,TicketStatus,TicketType,ImageData,ContentType")] Ticket ticket, string projectOptionSelect, IFormFile attachmentFile)
        {
            

            if (ModelState.IsValid)
            {
                var submitterId = _userManager.GetUserId(User);

                var projectName = await _context.Projects.FirstOrDefaultAsync(p => p.Name == projectOptionSelect);

                ticket.ProjectId = projectName.Id;

                ticket.ProjectUserId = submitterId;

                //get projectId by passing it into the HttpGet from wherever it was accessed
                //should be from a project details page

                ticket.Created = DateTime.Now;
                ticket.Updated = DateTime.Now;

                var newTicketHistory = new TicketHistory(ticket.Id);
                
                //create TicketSnapshot to add to TicketHistory
                var newTicketSnapshot = new TicketSnapshot();
                newTicketSnapshot.Title = ticket.Title;
                newTicketSnapshot.Body = ticket.Body;
                newTicketSnapshot.Created = DateTime.Now;
                newTicketSnapshot.ImageData = ticket.ImageData;
                newTicketSnapshot.ContentType = ticket.ContentType;
                newTicketSnapshot.TicketType = ticket.TicketType;
                newTicketHistory.TicketSnapshots.Add(newTicketSnapshot);

                ticket.TicketHistory = newTicketHistory;

                //Create TicketAttachment
                var newTicketAttachment = new TicketAttachment(ticket.Id);
                newTicketAttachment.AttachmentFile = attachmentFile;
                newTicketAttachment.ImageData = await _imageService.EncodeImageAsync(newTicketAttachment.AttachmentFile);
                newTicketAttachment.ContentType = _imageService.ContentType(newTicketAttachment.AttachmentFile);
                newTicketAttachment.TicketHistory = newTicketHistory;

                ticket.TicketAttachments.Add(newTicketAttachment);

                
                
                //add ticket, which automatically adds ticketAttachment and ticketHistory
                await _context.AddAsync(ticket);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", ticket.ProjectId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Project)
                .Include(t => t.TicketHistory)
                .Include(t => t.TicketHistory.TicketSnapshots)
                .Include(t => t.TicketAttachments)
                .Include(t => t.TicketComments)
                .FirstOrDefaultAsync(t => t.Id == id);

            

            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", ticket.ProjectId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectId,Title,Body,Resolved,TicketStatus")] Ticket ticket, int ticketHistoryFK)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ticket.Updated = DateTime.Now;

                    var newTicketSnapshot = new TicketSnapshot();
                    newTicketSnapshot.Title = ticket.Title;
                    newTicketSnapshot.Body = ticket.Body;
                    newTicketSnapshot.Created = ticket.Updated;
                    newTicketSnapshot.ImageData = ticket.ImageData;
                    newTicketSnapshot.ContentType = ticket.ContentType;
                    newTicketSnapshot.TicketStatus = ticket.TicketStatus;
                    //YOU CAN'T ACCESS TICKETHISTORY NAVIGATION PROPERTIES THIS WAY

                    _context.Update(ticket);

                    var ticketHistory = await _context.TicketHistories.FirstOrDefaultAsync(th => th.Id == ticketHistoryFK);
                    ticketHistory.TicketSnapshots.Add(newTicketSnapshot);

                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", ticket.ProjectId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
