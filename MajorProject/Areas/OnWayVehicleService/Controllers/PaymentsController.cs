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
    public class PaymentsController : Controller
    {
        private readonly MajorProjectDbContext _context;

        public PaymentsController(MajorProjectDbContext context)
        {
            _context = context;
        }

        // GET: OnWayVehicleService/Payments
        [Authorize(Roles = "RoleAdmin")]
        public async Task<IActionResult> Index()
        {
            var majorProjectDbContext = _context.Payments
                .Include(p => p.PaymentModes)
                .Include(p => p.ServiceBookings)
                .Include(p => p.ServiceBookings.Services)
                .Include(p => p.ServiceBookings.Issues.Cars);
            return View(await majorProjectDbContext.ToListAsync());
        }

        // GET: OnWayVehicleService/Payments/Details/5
        [Authorize(Roles = "RoleAdmin,RoleUser")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.PaymentModes)
                .Include(p => p.ServiceBookings)
                .Include(p => p.ServiceBookings.Services)
                .Include(p => p.ServiceBookings.Issues.Cars)
                .Include(p => p.ServiceBookings.Issues.Cars.Customers)
                .Include(p => p.ServiceBookings.Issues.Cars.CarModels)
                .Include(p => p.ServiceBookings.Issues.Cars.CarModels.CarCompanies)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: OnWayVehicleService/Payments/Create
        [Authorize(Roles = "RoleUser")]
        public IActionResult Create()
        {
            ViewData["PaymentMethodID"] = new SelectList(_context.PaymentModes, "PaymentModeID", "PaymentModes");
            ViewData["ServiceBookingID"] = new SelectList(_context.ServiceBookings, "ServiceBookingId", "ServiceBookingId");
            return View();
        }

        // POST: OnWayVehicleService/Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "RoleUser")]
        public async Task<IActionResult> Create([Bind("PaymentId,ServiceBookingID,PaymentMethodID,UPIID,PStatus")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                bool onetimecheck = _context.Payments.Any(m=>m.ServiceBookingID==payment.ServiceBookingID);
                bool check = payment.PStatus;
                if(onetimecheck)
                {
                    ModelState.AddModelError("ServiceBookingID", "PaymentAlreadyDone");

                }
                else if (!check)
                {
                    ModelState.AddModelError("PStatus", "Confirm Payment");

                }
                else
                {

                    _context.Add(payment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { id = payment.PaymentId });
                }
            }
            ViewData["PaymentMethodID"] = new SelectList(_context.PaymentModes, "PaymentModeID", "PaymentModes", payment.PaymentMethodID);
            ViewData["ServiceBookingID"] = new SelectList(_context.ServiceBookings, "ServiceBookingId", "ServiceBookingId", payment.ServiceBookingID);
            return View(payment);
        }

        // GET: OnWayVehicleService/Payments/Edit/5
        [Authorize(Roles = "RoleAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["PaymentMethodID"] = new SelectList(_context.PaymentModes, "PaymentModeID", "PaymentModes", payment.PaymentMethodID);
            ViewData["ServiceBookingID"] = new SelectList(_context.ServiceBookings, "ServiceBookingId", "ServiceBookingId", payment.ServiceBookingID);
            return View(payment);
        }

        // POST: OnWayVehicleService/Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "RoleAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,ServiceBookingID,PaymentMethodID,UPIID,PStatus")] Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentId))
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
            ViewData["PaymentMethodID"] = new SelectList(_context.PaymentModes, "PaymentModeID", "PaymentModes", payment.PaymentMethodID);
            ViewData["ServiceBookingID"] = new SelectList(_context.ServiceBookings, "ServiceBookingId", "ServiceBookingId", payment.ServiceBookingID);
            return View(payment);
        }

        // GET: OnWayVehicleService/Payments/Delete/5
        [Authorize(Roles = "RoleAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.PaymentModes)
                .Include(p => p.ServiceBookings)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: OnWayVehicleService/Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "RoleAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.PaymentId == id);
        }
    }
}
