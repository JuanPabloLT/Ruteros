using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ruteros.Common.Models;
using Ruteros.Web.Data;
using Ruteros.Web.Data.Entities;
using Ruteros.Web.Helpers;

namespace Ruteros.Web.Controllers.API
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;

        public TripsController(DataContext context, IUserHelper userHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
        }

        [HttpPost]
       public async Task<IActionResult> PostTripEntity([FromBody] TripRequest tripRequest)
       {
           if (!ModelState.IsValid)
           {
               return BadRequest(ModelState);
           }

           UserEntity userEntity = await _userHelper.GetUserAsync(tripRequest.UserId);
           if (userEntity == null)
           {
               return BadRequest("User doesn't exists.");
           }

           VehicleEntity vehicleEntity = await _context.Vehicles.FirstOrDefaultAsync(v => v.Plaque == tripRequest.Plaque);
            if (vehicleEntity == null)
            {
                return BadRequest("Vehicle doesn't exists.");
            }

            ShippingEntity shippingEntity = await _context.Shippings.FirstOrDefaultAsync(s => s.Code == tripRequest.ShippingCode);
            if (shippingEntity == null)
            {
                return BadRequest("Shipping doesn't exists.");
            }

            WarehouseEntity warehouseEntity = await _context.Warehouses.FirstOrDefaultAsync(w => w.Id == tripRequest.WarehouseId);
            if (warehouseEntity == null)
            {
                return BadRequest("Warehouse doesn't exists.");
            }

            TripEntity tripEntity = new TripEntity
            {
                Source = tripRequest.Address,
                SourceLatitude = tripRequest.Latitude,
                SourceLongitude = tripRequest.Longitude,
                StartDate = DateTime.UtcNow,
                TripDetails = new List<TripDetailEntity>
                {
                    new TripDetailEntity
                    {
                        Date = DateTime.UtcNow,
                        Latitude = tripRequest.Latitude,
                        Longitude = tripRequest.Longitude
                    }
                },
                User = userEntity,
                Vehicle = vehicleEntity,
                Warehouse = warehouseEntity,
                Shipping = shippingEntity,
            };

           _context.Trips.Add(tripEntity);
           await _context.SaveChangesAsync();
           return Ok(_converterHelper.ToTripResponse(tripEntity));
       }

        [HttpPost]
        [Route("CompleteTrip")]
        public async Task<IActionResult> CompleteTrip([FromBody] CompleteTripRequest completeTripRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TripEntity trip = await _context.Trips
                .Include(t => t.TripDetails)
                .FirstOrDefaultAsync(t => t.Id == completeTripRequest.TripId);
            if (trip == null)
            {
                return BadRequest("Trip not found.");
            }

            trip.EndDate = DateTime.UtcNow;
            trip.Remarks = completeTripRequest.Remarks;
            trip.Target = completeTripRequest.Target;
            trip.TargetLatitude = completeTripRequest.TargetLatitude;
            trip.TargetLongitude = completeTripRequest.TargetLongitude;
            trip.TripDetails.Add(new TripDetailEntity
            {
                Date = DateTime.UtcNow,
                Latitude = completeTripRequest.TargetLatitude,
                Longitude = completeTripRequest.TargetLongitude
            });
            _context.Trips.Update(trip);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTripEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TripEntity tripEntity = await _context.Trips
                .Include(t => t.TripDetails)
                .Include(t => t.Shipping)
                .ThenInclude(t => t.ShippingDetails)
                .Include(t => t.Vehicle)
                .Include(t => t.Warehouse)
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (tripEntity == null)
            {
                return BadRequest("Trip not found.");
            }

            return Ok(_converterHelper.ToTripResponse(tripEntity));
        }

        /*[HttpPost]
        [Route("GetMyTrips")]
        public async Task<IActionResult> GetMyTrips([FromBody] MyTripsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tripEntities = await _context.Trips
                .Include(t => t.User)
                .Include(t => t.TripDetails)
                .Include(t => t.Taxi)
                .Where(t => t.User.Id == request.UserId &&
                            t.StartDate >= request.StartDate &&
                            t.StartDate <= request.EndDate)
                .OrderByDescending(t => t.StartDate)
                .ToListAsync();

            return Ok(_converterHelper.ToTripResponse(tripEntities));
        }*/

        /*[HttpPost]
        [Route("AddTripDetails")]
        public async Task<IActionResult> AddTripDetails([FromBody] TripDetailsRequest tripDetailsRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (tripDetailsRequest.TripDetails == null || tripDetailsRequest.TripDetails.Count == 0)
            {
                return NoContent();
            }

            TripEntity trip = await _context.Trips
                .Include(t => t.TripDetails)
                .FirstOrDefaultAsync(t => t.Id == tripDetailsRequest.TripDetails.FirstOrDefault().TripId);
            if (trip == null)
            {
                return BadRequest("Trip not found.");
            }

            if (trip.TripDetails == null)
            {
                trip.TripDetails = new List<TripDetailEntity>();
            }

            foreach (TripDetailRequest tripDetailRequest in tripDetailsRequest.TripDetails)
            {
                trip.TripDetails.Add(new TripDetailEntity
                {
                    Date = DateTime.UtcNow,
                    Latitude = tripDetailRequest.Latitude,
                    Longitude = tripDetailRequest.Longitude
                });
            }

            _context.Trips.Update(trip);
            await _context.SaveChangesAsync();
            return NoContent();
        }*/

        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTripEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tripEntity = await _context.Trips
                .Include(t => t.TripDetails)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (tripEntity == null)
            {
                return NotFound();
            }

            _context.Trips.Remove(tripEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/






    }
}
