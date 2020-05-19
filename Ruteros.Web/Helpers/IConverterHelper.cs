using Ruteros.Web.Data.Entities;
using Ruteros.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruteros.Web.Helpers
{
    public interface IConverterHelper
    {
        VehicleEntity ToVehicleEntity(VehicleViewModel model, string path, bool isNew);
        VehicleViewModel ToVehicleViewModel(VehicleEntity vehicleEntity);
        Task<ShippingDetailEntity> ToShippingDetailEntity(ShippingDetailViewModel model, string path, bool isNew);
        ShippingDetailViewModel ToShippingDetailViewModel(ShippingDetailEntity shippingDetailEntity);
    }
}
