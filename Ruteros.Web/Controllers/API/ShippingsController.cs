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
    public class ShippingsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;

        public ShippingsController(DataContext context, IUserHelper userHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
        }

        [HttpPost]
        [Route("GetShippings")]
        public async Task<IActionResult> GetShippings([FromBody] ShippingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shippingEntities = await _context.Shippings
                .Include(s => s.ShippingDetails)
                .Where(s => s.Code == request.Shipping)
                .ToListAsync();

            return Ok(_converterHelper.ToShippingResponse(shippingEntities));
        }

        [HttpPost]
        [Route("GetShippingDetails")]
        public async Task<IActionResult> GetShippingDetails([FromBody] ShippingDetailRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shippingDetailEntities = await _context.ShippingDetails
                .Include(s => s.Shipping)
                .Where(s => s.Shipping.Id == request.Shippings)
                .ToListAsync();

            return Ok(_converterHelper.ToShippingDetailResponse(shippingDetailEntities));
        }

    }
}
