using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MajorProject.Data;
using MajorProject.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace MajorProject.Areas.OnWayVehicleService.Controllers
{
    [Area("OnWayVehicleService")]
    [Authorize(Roles = "RoleAdmin")]
    public class IssueCategoriesController : Controller
    {
        private readonly MajorProjectDbContext _context;

        public IssueCategoriesController(MajorProjectDbContext context)
        {
            _context = context;
        }

        // GET: OnWayVehicleService/IssueCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.IssueCategories.ToListAsync());
        }

        // GET: OnWayVehicleService/IssueCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueCategory = await _context.IssueCategories
                .FirstOrDefaultAsync(m => m.IssueCategoryId == id);
            if (issueCategory == null)
            {
                return NotFound();
            }

            return View(issueCategory);
        }

        // GET: OnWayVehicleService/IssueCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OnWayVehicleService/IssueCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IssueCategoryId,Issue")] IssueCategory issueCategory)
        {
            if (ModelState.IsValid)
            {

                issueCategory.Issue = issueCategory.Issue.Trim();
                bool ifduplicatefound = _context.IssueCategories.Any(m => m.Issue == issueCategory.Issue);
                if (ifduplicatefound)
                {
                    ModelState.AddModelError("Issue", "Already exsist!");
                }
                else
                {

                    _context.Add(issueCategory);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(issueCategory);
        }

        // GET: OnWayVehicleService/IssueCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueCategory = await _context.IssueCategories.FindAsync(id);
            if (issueCategory == null)
            {
                return NotFound();
            }
            return View(issueCategory);
        }

        // POST: OnWayVehicleService/IssueCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IssueCategoryId,Issue")] IssueCategory issueCategory)
        {
            if (id != issueCategory.IssueCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                issueCategory.Issue = issueCategory.Issue.Trim();
                bool dupc = _context.IssueCategories.Any(m => m.Issue == issueCategory.Issue && m.IssueCategoryId !=issueCategory.IssueCategoryId);
                if (dupc)
                {
                    ModelState.AddModelError("Issue", "Already Exsist!");

                }
                else
                {
                    try
                    {
                        _context.Update(issueCategory);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));

                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!IssueCategoryExists(issueCategory.IssueCategoryId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                }
            }
            return View(issueCategory);
        }

        // GET: OnWayVehicleService/IssueCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueCategory = await _context.IssueCategories
                .FirstOrDefaultAsync(m => m.IssueCategoryId == id);
            if (issueCategory == null)
            {
                return NotFound();
            }

            return View(issueCategory);
        }

        // POST: OnWayVehicleService/IssueCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issueCategory = await _context.IssueCategories.FindAsync(id);
            _context.IssueCategories.Remove(issueCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueCategoryExists(int id)
        {
            return _context.IssueCategories.Any(e => e.IssueCategoryId == id);
        }
    }
}
