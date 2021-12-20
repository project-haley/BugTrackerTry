using BugTrackerTry.Data;
using BugTrackerTry.Models;
using BugTrackerTry.Services;
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
    public class TicketAttachmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;
        private readonly UserManager<ProjectUser> _userManager;

        public TicketAttachmentsController(ApplicationDbContext context, IImageService imageService, UserManager<ProjectUser> userManager)
        {
            _context = context;
            _imageService = imageService;
            _userManager = userManager;
        }
        // GET: TicketAttachmentsController
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TicketAttachments;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TicketAttachmentsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TicketAttachmentsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketAttachmentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async void Create([Bind("Text,Image")] TicketAttachment ticketAttachment)
        {
            if (ModelState.IsValid)
            {
                ticketAttachment.Created = DateTime.Now;
                ticketAttachment.Updated = DateTime.Now;

                var ticketHistory = await _context.TicketHistories.FirstOrDefaultAsync(th => th.TicketId == ticketAttachment.TicketId);
                ticketAttachment.TicketHistoryId = ticketHistory.Id;

                _context.Add(ticketAttachment);
                await _context.SaveChangesAsync();

            }

        }

        // GET: TicketAttachmentsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TicketAttachmentsController/Edit/5
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

        // GET: TicketAttachmentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TicketAttachmentsController/Delete/5
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
