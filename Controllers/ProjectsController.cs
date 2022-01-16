﻿using System;
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
using BugTrackerTry.Services;
using Microsoft.AspNetCore.Http;

namespace BugTrackerTry.Controllers
{

    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ProjectUser> _userManager;
        private readonly IImageService _imageService;

        public ProjectsController(ApplicationDbContext context, UserManager<ProjectUser> userManager, IImageService imageService)
        {
            _context = context;
            _userManager = userManager;
            _imageService = imageService;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var projectList = await _context.Projects
                .Include(p => p.ProjectUsers)
                .Include(p => p.Tickets)
                .ToListAsync();
            return View(projectList.OrderByDescending(p => p.Name).Reverse());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Roles = "Administrator, ProjectManager")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, ProjectManager")]
        public async Task<IActionResult> Create([Bind("Name,Description,Image,ProjectUsers")] Project project, List<string> userList, IFormFile attachmentFile)
        {
            if (ModelState.IsValid)
            {
                var authorId = _userManager.GetUserId(User);

                project.Created = DateTime.Now;
                project.Updated = DateTime.Now;
                project.ProjectUserId = authorId;
                project.Image = attachmentFile;
                project.ImageData = await _imageService.EncodeImageAsync(project.Image);
                project.ContentType = _imageService.ContentType(project.Image);


                //First, add project to db?
                _context.Add(project);
                await _context.SaveChangesAsync();

                //Then, attach navigation properties to project?
                var newProject = await _context.Projects
                    .FirstOrDefaultAsync(p => p.Id == project.Id);


                foreach (var userOption in userList)
                {
                    // I think this works, but can't display them anywhere...
                    // Update: it is working, and project still contains projectUsers up until savechanges...
                    var targetUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userOption);
                    newProject.ProjectUsers.Add(targetUser);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectUserId,Name,Description,Created,Updated,ImageData,ContentType")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
