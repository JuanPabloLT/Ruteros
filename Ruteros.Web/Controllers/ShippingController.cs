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
    [Authorize]
    public class ShippingController : Controller
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;

        public ShippingController(DataContext context,
            IConverterHelper converterHelper,
            IImageHelper imageHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
        }

        // GET: Shipping
        public async Task<IActionResult> Index()
        {
            return View(await _context
                .Shippings
                .Include(s => s.ShippingDetails)
                .ToListAsync());
        }

        // GET: Shipping/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ShippingEntity shippingEntity = await _context
                .Shippings
                .Include(s => s.ShippingDetails)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shippingEntity == null)
            {
                return NotFound();
            }

            return View(shippingEntity);
        }

        [Authorize(Roles = "Admin")]
        // GET: Shipping/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shipping/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code")] ShippingEntity shippingEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shippingEntity);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Already exists a shipping with the same code.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
            }
            return View(shippingEntity);
        }


        [Authorize(Roles = "Admin")]
        // GET: Shipping/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingEntity = await _context.Shippings.FindAsync(id);
            if (shippingEntity == null)
            {
                return NotFound();
            }
            return View(shippingEntity);
        }

        // POST: Shipping/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code")] ShippingEntity shippingEntity)
        {
            if (id != shippingEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                _context.Update(shippingEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Already exists a shipping with the same code.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
            }
            return View(shippingEntity);
        }

        // GET: Shipping/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingEntity = await _context.Shippings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shippingEntity == null)
            {
                return NotFound();
            }

            return View(shippingEntity);
        }

        // POST: Shipping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shippingEntity = await _context.Shippings.FindAsync(id);
            _context.Shippings.Remove(shippingEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShippingEntityExists(int id)
        {
            return _context.Shippings.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Admin")]
        // GET: Shipping/Create
        public async Task<IActionResult> CreateDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ShippingEntity shippingEntity = await _context.Shippings.FindAsync(id);
            if (shippingEntity == null)
            {
                return NotFound();
            }

            ShippingDetailViewModel model = new ShippingDetailViewModel
            {
                Shipping = shippingEntity,
                ShippingId = shippingEntity.Id
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDetail(ShippingDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = string.Empty;

                if (model.PictureFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.PictureFile, "ShippingDetails");
                }
                ShippingDetailEntity shippingDetailEntity = await _converterHelper.ToShippingDetailEntity(model, path, true);
                _context.Add(shippingDetailEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}/{model.ShippingId}");
            }

            return View(model);
        }

    }
}
