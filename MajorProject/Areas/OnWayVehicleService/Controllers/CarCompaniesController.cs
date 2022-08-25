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
    [Area("OnWayVehicleService")]
    [Authorize(Roles ="RoleAdmin")]
    public class CarCompaniesController : Controller
    {
        private readonly MajorProjectDbContext _context;

        public CarCompaniesController(MajorProjectDbContext context)
        {
            _context = context;
        }

        // GET: OnWayVehicleService/CarCompanies
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarCompany.ToListAsync());
        }

        // GET: OnWayVehicleService/CarCompanies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carCompany = await _context.CarCompany
                .FirstOrDefaultAsync(m => m.CarCompanyId == id);
            if (carCompany == null)
            {
                return NotFound();
            }

            return View(carCompany);
        }

        // GET: OnWayVehicleService/CarCompanies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OnWayVehicleService/CarCompanies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarCompanyId,CarCompanies")] CarCompany carCompany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(carCompany);
        }

        // GET: OnWayVehicleService/CarCompanies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carCompany = await _context.CarCompany.FindAsync(id);
            if (carCompany == null)
            {
                return NotFound();
            }
            return View(carCompany);
        }

        // POST: OnWayVehicleService/CarCompanies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarCompanyId,CarCompanies")] CarCompany carCompany)
        {
            if (id != carCompany.CarCompanyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarCompanyExists(carCompany.CarCompanyId))
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
            return View(carCompany);
        }

        // GET: OnWayVehicleService/CarCompanies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carCompany = await _context.CarCompany
                .FirstOrDefaultAsync(m => m.CarCompanyId == id);
            if (carCompany == null)
            {
                return NotFound();
            }

            return View(carCompany);
        }

        // POST: OnWayVehicleService/CarCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carCompany = await _context.CarCompany.FindAsync(id);
            _context.CarCompany.Remove(carCompany);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarCompanyExists(int id)
        {
            return _context.CarCompany.Any(e => e.CarCompanyId == id);
        }
    }
}
