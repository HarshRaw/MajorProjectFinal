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
    [Authorize(Roles = "RoleUser")]
    public class CustomersController : Controller
    {
        private readonly MajorProjectDbContext _context;

        public CustomersController(MajorProjectDbContext context)
        {
            _context = context;
        }

        // GET: OnWayVehicleService/Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }

        // GET: OnWayVehicleService/Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: OnWayVehicleService/Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OnWayVehicleService/Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CustomerName,MobileNumber,Email,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.MobileNumber = customer.MobileNumber.Trim();
                customer.Email = customer.Email.Trim();

                bool ifduplicatefound2 = _context.Customers.Any(m => m.Email == customer.Email);
                bool ifduplicatefound1 = _context.Customers.Any(m => m.MobileNumber == customer.MobileNumber);
                if (ifduplicatefound1)
                {
                    ModelState.AddModelError("MobileNumber", "This Mobile Number Already exsist!");
                }
                else if (ifduplicatefound2)
                {

                    ModelState.AddModelError("Email", "This Mail Already exsist!");
                }
                else
                {
                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { id = customer.CustomerId });
                }
                
            }
            return View(customer);
        }

        // GET: OnWayVehicleService/Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: OnWayVehicleService/Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,CustomerName,MobileNumber,Email,Address")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                customer.Email = customer.Email.Trim();

                bool dupf2 = _context.Customers.Any(a => a.Email == customer.Email && a.CustomerId != customer.CustomerId);
                bool dupf1 = _context.Customers.Any(a => a.MobileNumber == customer.MobileNumber && a.CustomerId != customer.CustomerId);

                if (dupf1)
                {
                    ModelState.AddModelError("MobileNumber", "Already Exsist!");

                }
                else if (dupf2)
                {

                    ModelState.AddModelError("Email", "Already Exsist!");
                }
                else
                {
                    try
                    {
                        _context.Update(customer);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Details",new {id = customer.CustomerId });

                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CustomerExists(customer.CustomerId))
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
            return View(customer);
        }

        // GET: OnWayVehicleService/Customers/Delete/5
        [Authorize(Roles = "RoleAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: OnWayVehicleService/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "RoleAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
