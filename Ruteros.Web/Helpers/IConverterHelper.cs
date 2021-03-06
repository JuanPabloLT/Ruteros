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
        //RESPONSES
        UserResponse ToUserResponse(UserEntity user);
        TripResponse ToTripResponse(TripEntity tripEntity);
        List<TripResponse> ToTripResponse(List<TripEntity> tripEntities);
        WarehouseResponse ToWarehouseResponse(WarehouseEntity warehouseEntity);
        ShippingResponse ToShippingResponse(ShippingEntity shippingEntity);
        List<ShippingResponse> ToShippingResponse(List<ShippingEntity> shippingEntities);
        VehicleResponse ToVehicleResponse(VehicleEntity vehicleEntity);
        ShippingDetailResponse ToShippingDetailResponse(ShippingDetailEntity shippingDetailEntity);
        List<ShippingDetailResponse> ToShippingDetailResponse(List<ShippingDetailEntity> shippingDetailEntities);
        TripDetailResponse ToTripDetailResponse(TripDetailEntity tripDetailEntity);

        //ENTITIES AND VIEW MODELS
        VehicleEntity ToVehicleEntity(VehicleViewModel model, string path, bool isNew);
        VehicleViewModel ToVehicleViewModel(VehicleEntity vehicleEntity);
        Task<ShippingDetailEntity> ToShippingDetailEntity(ShippingDetailViewModel model, string path, bool isNew);
        ShippingDetailViewModel ToShippingDetailViewModel(ShippingDetailEntity shippingDetailEntity);
    }
}
