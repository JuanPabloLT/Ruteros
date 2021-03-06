using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ruteros.Web.Data;
using Ruteros.Web.Data.Entities;
using Ruteros.Web.Helpers;
using Ruteros.Web.Models;

namespace Ruteros.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VehiclesController : Controller
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;

        public VehiclesController(DataContext context,
            IConverterHelper converterHelper,
            IImageHelper imageHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vehicles.ToListAsync());
        }

        

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleViewModel vehicleViewModel)
        {
            if (ModelState.IsValid)
            {
                string path = string.Empty;

                if (vehicleViewModel.PictureFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(vehicleViewModel.PictureFile, "Vehicles");
                }

                VehicleEntity vehicleEntity = _converterHelper.ToVehicleEntity(vehicleViewModel, path, true);
                _context.Add(vehicleEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Already exists a vehicle with the same plaque.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
            }

            return View(vehicleViewModel);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VehicleEntity vehicleEntity = await _context.Vehicles.FindAsync(id);
            if (vehicleEntity == null)
            {
                return NotFound();
            }

            VehicleViewModel vehicleViewModel = _converterHelper.ToVehicleViewModel(vehicleEntity);
            return View(vehicleViewModel);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleViewModel vehicleViewModel)
        {
            if (id != vehicleViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string path = vehicleViewModel.PicturePath;

                if (vehicleViewModel.PictureFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(vehicleViewModel.PictureFile, "Vehicles");
                }

                VehicleEntity vehicleEntity = _converterHelper.ToVehicleEntity(vehicleViewModel, path, false);
                _context.Update(vehicleEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, $"Already exists the vehicle: {vehicleEntity.Plaque}.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
            }
            return View(vehicleViewModel);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VehicleEntity vehicleEntity = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleEntity == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicleEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
