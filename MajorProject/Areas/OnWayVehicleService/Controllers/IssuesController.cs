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

namespace MajorProject.Areas.OnWayVehicleService.Controllers
{
    [Authorize]
    [Area("OnWayVehicleService")]
    public class IssuesController : Controller
    {
        private readonly MajorProjectDbContext _context;

        public IssuesController(MajorProjectDbContext context)
        {
            _context = context;
        }

        // GET: OnWayVehicleService/Issues
        public async Task<IActionResult> Index()
        {
            var majorProjectDbContext = _context.Issues.Include(i => i.Cars).Include(i => i.Cars.Customers).Include(i => i.IssueCategories).Include(i => i.Urgencies);
            return View(await majorProjectDbContext.ToListAsync());
        }

        // GET: OnWayVehicleService/Issues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .Include(i => i.Cars)
                .Include(i => i.IssueCategories)
                .Include(i => i.Urgencies)
                .FirstOrDefaultAsync(m => m.IssueId == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // GET: OnWayVehicleService/Issues/Create
        public IActionResult Create()
        {
            ViewData["Car"] = new SelectList(_context.Cars, "CarId", "CarNumber");
            ViewData["IssueCategory"] = new SelectList(_context.IssueCategories, "IssueCategoryId", "Issue");
            ViewData["Urgency"] = new SelectList(_context.Urgencies, "UrgencyId", "Urgencies");
            return View();
        }

        // POST: OnWayVehicleService/Issues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IssueId,Car,Urgency,IssueCategory,Services")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issue);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new {id = issue.IssueId});
            }
            ViewData["Car"] = new SelectList(_context.Cars, "CarId", "CarNumber", issue.Car);
            ViewData["IssueCategory"] = new SelectList(_context.IssueCategories, "IssueCategoryId", "Issue", issue.IssueCategory);
            ViewData["Urgency"] = new SelectList(_context.Urgencies, "UrgencyId", "Urgencies", issue.Urgency);
            return View(issue);
        }

        // GET: OnWayVehicleService/Issues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }
            ViewData["Car"] = new SelectList(_context.Cars, "CarId", "CarNumber", issue.Car);
            ViewData["IssueCategory"] = new SelectList(_context.IssueCategories, "IssueCategoryId", "Issue", issue.IssueCategory);
            ViewData["Urgency"] = new SelectList(_context.Urgencies, "UrgencyId", "Urgencies", issue.Urgency);
            return View(issue);
        }

        // POST: OnWayVehicleService/Issues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IssueId,Car,Urgency,IssueCategory,Services")] Issue issue)
        {
            if (id != issue.IssueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueExists(issue.IssueId))
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
            ViewData["Car"] = new SelectList(_context.Cars, "CarId", "CarNumber", issue.Car);
            ViewData["IssueCategory"] = new SelectList(_context.IssueCategories, "IssueCategoryId", "Issue", issue.IssueCategory);
            ViewData["Urgency"] = new SelectList(_context.Urgencies, "UrgencyId", "Urgencies", issue.Urgency);
            return View(issue);
        }

        // GET: OnWayVehicleService/Issues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .Include(i => i.Cars)
                .Include(i => i.IssueCategories)
                .Include(i => i.Urgencies)
                .FirstOrDefaultAsync(m => m.IssueId == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // POST: OnWayVehicleService/Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueExists(int id)
        {
            return _context.Issues.Any(e => e.IssueId == id);
        }
    }
}
