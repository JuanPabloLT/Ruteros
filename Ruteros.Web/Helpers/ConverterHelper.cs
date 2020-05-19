using Ruteros.Web.Data;
using Ruteros.Web.Data.Entities;
using Ruteros.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruteros.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

        public async Task<ShippingDetailEntity> ToShippingDetailEntity(ShippingDetailViewModel model, string path, bool isNew)
        {
            return new ShippingDetailEntity
            {
                Id = isNew ? 0 : model.Id,
                PicturePath = path,
                Quantity = model.Quantity,
                Description = model.Description,
                Shipping = await _context.Shippings.FindAsync(model.ShippingId)
            };
        }

        public ShippingDetailViewModel ToShippingDetailViewModel(ShippingDetailEntity shippingDetailEntity)
        {
            return new ShippingDetailViewModel
            {
                Id = shippingDetailEntity.Id,
                PicturePath = shippingDetailEntity.PicturePath,
                Quantity = shippingDetailEntity.Quantity,
                Description = shippingDetailEntity.Description,
                Shipping = shippingDetailEntity.Shipping,
                ShippingId = shippingDetailEntity.Shipping.Id
            };
        }

        public VehicleEntity ToVehicleEntity(VehicleViewModel model, string path, bool isNew)
        {
            return new VehicleEntity
            {
                Id = isNew ? 0 : model.Id,
                PicturePath = path,
                Plaque = model.Plaque
            };
        }

        
        public VehicleViewModel ToVehicleViewModel(VehicleEntity vehicleEntity)
        {
            return new VehicleViewModel
            {
                Id = vehicleEntity.Id,
                PicturePath = vehicleEntity.PicturePath,
                Plaque = vehicleEntity.Plaque
            };
        }
    }
}
