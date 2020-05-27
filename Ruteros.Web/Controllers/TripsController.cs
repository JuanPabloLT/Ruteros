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
    public class TripsController : Controller
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;

        public TripsController(DataContext context,
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
                .Trips
                .Include(t => t.TripDetails)
                .Include(t => t.Vehicle)
                .Include(t => t.User)
                .Include(t => t.Warehouse)
                .Include(t => t.Shipping)
                .ThenInclude(s => s.ShippingDetails)
                //.Where(t => t.User.Document == ""))
                .ToListAsync());
        }

        // GET: Trips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TripEntity tripEntity = await _context
                .Trips
                .Include(t => t.TripDetails)
                .Include(t => t.Vehicle)
                .Include(t => t.User)
                .Include(t => t.Warehouse)
                .Include(t => t.Shipping)
                .ThenInclude(s => s.ShippingDetails)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripEntity == null)
            {
                return NotFound();
            }

            return View(tripEntity);
        }


    }
}
