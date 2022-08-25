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
    [Authorize(Roles = "RoleAdmin,RoleUser")]
    public class CarsController : Controller
    {
        private readonly MajorProjectDbContext _context;

        public CarsController(MajorProjectDbContext context)
        {
            _context = context;
        }

        // GET: OnWayVehicleService/Cars
        public async Task<IActionResult> Index()
        {
            var majorProjectDbContext = _context.Cars.Include(c => c.CarModels).Include(c => c.CarModels.CarCompanies).Include(c => c.Customers);
            return View(await majorProjectDbContext.ToListAsync());
        }

        // GET: OnWayVehicleService/Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarModels)
                .Include(c => c.CarModels.CarCompanies)
                .Include(c => c.Customers)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: OnWayVehicleService/Cars/Create
        public IActionResult Create()
        {
            ViewData["CarMID"] = new SelectList(_context.Set<CarModel>(), "CarCompanyId", "CarModels");
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerId", "CustomerName");
            ViewData["CarCID"] = new SelectList(_context.CarCompany, "CarCompanyId", "CarCompanies");

            return View();
        }

        // POST: OnWayVehicleService/Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarId,CarMID,CarNumber,CustomerID")] Car car)
        {
            if (ModelState.IsValid)
            {
                car.CarNumber = car.CarNumber.Trim();
                bool ifduplicatefound = _context.Cars.Any(m => m.CarNumber == car.CarNumber);
                if (ifduplicatefound)
                {
                    ModelState.AddModelError("CarNumber","Already Exsist");
                }
                else
                {
                    _context.Add(car);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { id = car.CarId });
                }

            }
            ViewData["CarMID"] = new SelectList(_context.Set<CarModel>(), "CarCompanyId", "CarModels", car.CarMID);
            ViewData["CarCID"] = new SelectList(_context.CarCompany, "CarCompanyId", "CarCompanies");

            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", car.CustomerID);
            return View(car);
        }

        // GET: OnWayVehicleService/Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            ViewData["CarMID"] = new SelectList(_context.Set<CarModel>(), "CarCompanyId", "CarModels", car.CarMID);
            ViewData["CarCID"] = new SelectList(_context.CarCompany, "CarCompanyId", "CarCompanies");

            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", car.CustomerID);
            return View(car);
        }

        // POST: OnWayVehicleService/Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId,CarMID,CarNumber,CustomerID")] Car car)
        {
            if (id != car.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarId))
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

            ViewData["CarMID"] = new SelectList(_context.Set<CarModel>(), "CarCompanyId", "CarModels", car.CarMID);
            ViewData["CarCID"] = new SelectList(_context.CarCompany, "CarCompanyId", "CarCompanies");
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", car.CustomerID);
            return View(car);
        }

        // GET: OnWayVehicleService/Cars/Delete/5
        [Authorize(Roles = "RoleAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarModels)
                .Include(c => c.Customers)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: OnWayVehicleService/Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "RoleAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.CarId == id);
        }
    }
}
