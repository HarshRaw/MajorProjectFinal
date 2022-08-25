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
    public class UrgenciesController : Controller
    {
        private readonly MajorProjectDbContext _context;

        public UrgenciesController(MajorProjectDbContext context)
        {
            _context = context;
        }

        // GET: OnWayVehicleService/Urgencies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Urgencies.ToListAsync());
        }

        // GET: OnWayVehicleService/Urgencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urgency = await _context.Urgencies
                .FirstOrDefaultAsync(m => m.UrgencyId == id);
            if (urgency == null)
            {
                return NotFound();
            }

            return View(urgency);
        }

        // GET: OnWayVehicleService/Urgencies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OnWayVehicleService/Urgencies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UrgencyId,Urgencies,TimeToReach")] Urgency urgency)
        {
            if (ModelState.IsValid)
            {
                _context.Add(urgency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(urgency);
        }

        // GET: OnWayVehicleService/Urgencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urgency = await _context.Urgencies.FindAsync(id);
            if (urgency == null)
            {
                return NotFound();
            }
            return View(urgency);
        }

        // POST: OnWayVehicleService/Urgencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UrgencyId,Urgencies,TimeToReach")] Urgency urgency)
        {
            if (id != urgency.UrgencyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(urgency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrgencyExists(urgency.UrgencyId))
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
            return View(urgency);
        }

        // GET: OnWayVehicleService/Urgencies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urgency = await _context.Urgencies
                .FirstOrDefaultAsync(m => m.UrgencyId == id);
            if (urgency == null)
            {
                return NotFound();
            }

            return View(urgency);
        }

        // POST: OnWayVehicleService/Urgencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var urgency = await _context.Urgencies.FindAsync(id);
            _context.Urgencies.Remove(urgency);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrgencyExists(int id)
        {
            return _context.Urgencies.Any(e => e.UrgencyId == id);
        }
    }
}
