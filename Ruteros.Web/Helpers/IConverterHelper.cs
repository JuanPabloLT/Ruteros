using Ruteros.Web.Data.Entities;
using Ruteros.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ruteros.Common.Models;

namespace Ruteros.Web.Helpers
{
    public interface IConverterHelper
    {
        UserResponse ToUserResponse(UserEntity user);
        VehicleEntity ToVehicleEntity(VehicleViewModel model, string path, bool isNew);
        VehicleViewModel ToVehicleViewModel(VehicleEntity vehicleEntity);
        Task<ShippingDetailEntity> ToShippingDetailEntity(ShippingDetailViewModel model, string path, bool isNew);
        ShippingDetailViewModel ToShippingDetailViewModel(ShippingDetailEntity shippingDetailEntity);
    }
}
