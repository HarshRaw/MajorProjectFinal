using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MajorProject.Data;
using MajorProject.Models;

namespace MajorProject.Areas.OnWayVehicleService.Controllers
{
    [Area("OnWayVehicleService")]
    public class ServiceBookingsController : Controller
    {
        private readonly MajorProjectDbContext _context;

        public ServiceBookingsController(MajorProjectDbContext context)
        {
            _context = context;
        }

        // GET: OnWayVehicleService/ServiceBookings
        public async Task<IActionResult> Index()
        {
            var majorProjectDbContext = _context.ServiceBookings.Include(s => s.Issues).Include(s => s.Services);
            return View(await majorProjectDbContext.ToListAsync());
        }

        public async Task<IActionResult> Index1()
        {
            var majorProjectDbContext = _context.ServiceBookings.
                Include(s => s.Issues)
                .Include(s => s.Services)
                .Include(s => s.Issues.Cars.Customers)
                ;
            return View(await majorProjectDbContext.ToListAsync());
        }


        // GET: OnWayVehicleService/ServiceBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceBooking = await _context.ServiceBookings
                .Include(s => s.Issues)
                .Include(s => s.Services)
                .FirstOrDefaultAsync(m => m.ServiceBookingId == id);
            if (serviceBooking == null)
            {
                return NotFound();
            }

            return View(serviceBooking);
        }
        public async Task<IActionResult> Details1(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceBooking = await _context.ServiceBookings
                .Include(s => s.Issues)
                .Include(s => s.Services).Include(s => s.Issues.Cars.Customers)
                .FirstOrDefaultAsync(m => m.ServiceBookingId == id);
            if (serviceBooking == null)
            {
                return NotFound();
            }

            return View(serviceBooking);
        }
        // GET: OnWayVehicleService/ServiceBookings/Create
        public IActionResult Create()
        {
            ViewData["Issue"] = new SelectList(_context.Issues, "IssueId", "IssueId");
            ViewData["Service"] = new SelectList(_context.Services, "ServiceID", "Services");
            return View();
        }

        // POST: OnWayVehicleService/ServiceBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceBookingId,DateCreated,Issue,Service,CurrentLocationOfCar")] ServiceBooking serviceBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = serviceBooking.ServiceBookingId });
            }
            ViewData["Issue"] = new SelectList(_context.Issues, "IssueId", "IssueId", serviceBooking.Issue);
            ViewData["Service"] = new SelectList(_context.Services, "ServiceID", "Services", serviceBooking.Service);
            return View(serviceBooking);
        }

        // GET: OnWayVehicleService/ServiceBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceBooking = await _context.ServiceBookings.FindAsync(id);
            if (serviceBooking == null)
            {
                return NotFound();
            }
            ViewData["Issue"] = new SelectList(_context.Issues, "IssueId", "IssueId", serviceBooking.Issue);
            ViewData["Service"] = new SelectList(_context.Services, "ServiceID", "Services", serviceBooking.Service);
            return View(serviceBooking);
        }

        // POST: OnWayVehicleService/ServiceBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceBookingId,DateCreated,Issue,Service,CurrentLocationOfCar")] ServiceBooking serviceBooking)
        {
            if (id != serviceBooking.ServiceBookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceBookingExists(serviceBooking.ServiceBookingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["Issue"] = new SelectList(_context.Issues, "IssueId", "IssueId", serviceBooking.Issue);
            ViewData["Service"] = new SelectList(_context.Services, "ServiceID", "Services", serviceBooking.Service);
            return View(serviceBooking);
        }

        // GET: OnWayVehicleService/ServiceBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceBooking = await _context.ServiceBookings
                .Include(s => s.Issues)
                .Include(s => s.Services)
                .FirstOrDefaultAsync(m => m.ServiceBookingId == id);
            if (serviceBooking == null)
            {
                return NotFound();
            }

            return View(serviceBooking);
        }

        // POST: OnWayVehicleService/ServiceBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceBooking = await _context.ServiceBookings.FindAsync(id);
            _context.ServiceBookings.Remove(serviceBooking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index1));
        }

        private bool ServiceBookingExists(int id)
        {
            return _context.ServiceBookings.Any(e => e.ServiceBookingId == id);
        }
    }
}
