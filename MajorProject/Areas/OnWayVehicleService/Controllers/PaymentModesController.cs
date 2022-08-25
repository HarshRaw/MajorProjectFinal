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
    public class PaymentModesController : Controller
    {
        private readonly MajorProjectDbContext _context;

        public PaymentModesController(MajorProjectDbContext context)
        {
            _context = context;
        }

        // GET: OnWayVehicleService/PaymentModes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaymentModes.ToListAsync());
        }

        // GET: OnWayVehicleService/PaymentModes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentMode = await _context.PaymentModes
                .FirstOrDefaultAsync(m => m.PaymentModeID == id);
            if (paymentMode == null)
            {
                return NotFound();
            }

            return View(paymentMode);
        }

        // GET: OnWayVehicleService/PaymentModes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OnWayVehicleService/PaymentModes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentModeID,PaymentModes,Available")] PaymentMode paymentMode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentMode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentMode);
        }

        // GET: OnWayVehicleService/PaymentModes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentMode = await _context.PaymentModes.FindAsync(id);
            if (paymentMode == null)
            {
                return NotFound();
            }
            return View(paymentMode);
        }

        // POST: OnWayVehicleService/PaymentModes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentModeID,PaymentModes,Available")] PaymentMode paymentMode)
        {
            if (id != paymentMode.PaymentModeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentMode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentModeExists(paymentMode.PaymentModeID))
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
            return View(paymentMode);
        }

        // GET: OnWayVehicleService/PaymentModes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentMode = await _context.PaymentModes
                .FirstOrDefaultAsync(m => m.PaymentModeID == id);
            if (paymentMode == null)
            {
                return NotFound();
            }

            return View(paymentMode);
        }

        // POST: OnWayVehicleService/PaymentModes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentMode = await _context.PaymentModes.FindAsync(id);
            _context.PaymentModes.Remove(paymentMode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentModeExists(int id)
        {
            return _context.PaymentModes.Any(e => e.PaymentModeID == id);
        }
    }
}
