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
    public class CarModelsController : Controller
    {
        private readonly MajorProjectDbContext _context;

        public CarModelsController(MajorProjectDbContext context)
        {
            _context = context;
        }

        // GET: OnWayVehicleService/CarModels
        public async Task<IActionResult> Index()
        {
            var majorProjectDbContext = _context.CarModel.Include(c => c.CarCompanies);
            return View(await majorProjectDbContext.ToListAsync());
        }

        // GET: OnWayVehicleService/CarModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModel
                .Include(c => c.CarCompanies)
                .FirstOrDefaultAsync(m => m.CarCompanyId == id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // GET: OnWayVehicleService/CarModels/Create
        public IActionResult Create()
        {
            ViewData["CarCID"] = new SelectList(_context.CarCompany, "CarCompanyId", "CarCompanies");
            return View();
        }

        // POST: OnWayVehicleService/CarModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarCompanyId,CarCID,CarModels")] CarModel carModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarCID"] = new SelectList(_context.CarCompany, "CarCompanyId", "CarCompanies", carModel.CarCID);
            return View(carModel);
        }

        // GET: OnWayVehicleService/CarModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModel.FindAsync(id);
            if (carModel == null)
            {
                return NotFound();
            }
            ViewData["CarCID"] = new SelectList(_context.CarCompany, "CarCompanyId", "CarCompanies", carModel.CarCID);
            return View(carModel);
        }

        // POST: OnWayVehicleService/CarModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarCompanyId,CarCID,CarModels")] CarModel carModel)
        {
            if (id != carModel.CarCompanyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarModelExists(carModel.CarCompanyId))
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
            ViewData["CarCID"] = new SelectList(_context.CarCompany, "CarCompanyId", "CarCompanies", carModel.CarCID);
            return View(carModel);
        }

        // GET: OnWayVehicleService/CarModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModel
                .Include(c => c.CarCompanies)
                .FirstOrDefaultAsync(m => m.CarCompanyId == id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // POST: OnWayVehicleService/CarModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carModel = await _context.CarModel.FindAsync(id);
            _context.CarModel.Remove(carModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarModelExists(int id)
        {
            return _context.CarModel.Any(e => e.CarCompanyId == id);
        }
    }
}
