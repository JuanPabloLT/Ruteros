using Ruteros.Web.Data;
using Ruteros.Web.Data.Entities;
using Ruteros.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ruteros.Common.Models;

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

        public TripResponse ToTripResponse(TripEntity tripEntity)
        {
            return new TripResponse
            {
                EndDate = tripEntity.EndDate,
                Id = tripEntity.Id,
                Remarks = tripEntity.Remarks,
                Source = tripEntity.Source,
                SourceLatitude = tripEntity.SourceLatitude,
                SourceLongitude = tripEntity.SourceLongitude,
                StartDate = tripEntity.StartDate,
                Target = tripEntity.Target,
                TargetLatitude = tripEntity.TargetLatitude,
                TargetLongitude = tripEntity.TargetLongitude,
                TripDetails = tripEntity.TripDetails?.Select(td => new TripDetailResponse
                {
                    Date = td.Date,
                    Id = td.Id,
                    Latitude = td.Latitude,
                    Longitude = td.Longitude
                }).ToList(),
                User = ToUserResponse(tripEntity.User),
                Vehicle = ToVehicleResponse(tripEntity.Vehicle),
                Warehouse = ToWarehouseResponse(tripEntity.Warehouse),
                Shipping = ToShippingResponse(tripEntity.Shipping)
            };
        }

        //TRIPResponseList
        public List<TripResponse> ToTripResponse(List<TripEntity> tripEntities)
        {
            return tripEntities.Select(t => new TripResponse
            {
                EndDate = t.EndDate,
                Id = t.Id,
                Remarks = t.Remarks,
                Source = t.Source,
                SourceLatitude = t.SourceLatitude,
                SourceLongitude = t.SourceLongitude,
                StartDate = t.StartDate,
                Target = t.Target,
                TargetLatitude = t.TargetLatitude,
                TargetLongitude = t.TargetLongitude,
                User = ToUserResponse(t.User),
                Vehicle = ToVehicleResponse(t.Vehicle),
                Warehouse = ToWarehouseResponse(t.Warehouse),
                Shipping = ToShippingResponse(t.Shipping),
                TripDetails = t.TripDetails.Select(td => new TripDetailResponse
                {
                    Date = td.Date,
                    Id = td.Id,
                    Latitude = td.Latitude,
                    Longitude = td.Longitude
                }).ToList()
            }).ToList();
        }


        public List<ShippingResponse> ToShippingResponse(List<ShippingEntity> shippingEntities)
        {
            return shippingEntities.Select(t => new ShippingResponse
            {
                Id = t.Id,
                Code = t.Code,
                ShippingDetails = t.ShippingDetails.Select(td => new ShippingDetailResponse
                {
                    Id = td.Id,
                    Quantity = td.Quantity,
                    Description = td.Description,
                    PicturePath = td.PicturePath
                }).ToList()
            }).ToList();
        }

        public List<ShippingDetailResponse> ToShippingDetailResponse(List<ShippingDetailEntity> shippingDetailEntities)
        {
            return shippingDetailEntities.Select(t => new ShippingDetailResponse
            {
                Id = t.Id,
                Quantity = t.Quantity,
                Description = t.Description,
                PicturePath = t.PicturePath
            }).ToList();
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

        public UserResponse ToUserResponse(UserEntity user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserResponse
            {
                Document = user.Document,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                PicturePath = user.PicturePath,
                UserType = user.UserType
            };
        }

        public WarehouseResponse ToWarehouseResponse(WarehouseEntity warehouseEntity)
        {
            if (warehouseEntity == null)
            {
                return null;
            }

            return new WarehouseResponse
            {
                Id = warehouseEntity.Id,
                Name = warehouseEntity.Name,
                Address = warehouseEntity.Address,
                Telephone = warehouseEntity.Telephone
            };
        }

        public ShippingResponse ToShippingResponse(ShippingEntity shippingEntity)
        {
            if (shippingEntity == null)
            {
                return null;
            }

            return new ShippingResponse
            {
                Id = shippingEntity.Id,
                Code = shippingEntity.Code,
                ShippingDetails = shippingEntity.ShippingDetails?.Select(sd => new ShippingDetailResponse
                {
                    Id = sd.Id,
                    Quantity = sd.Quantity,
                    Description = sd.Description,
                    PicturePath = sd.PicturePath
                }).ToList(),
            };
        }

        public VehicleResponse ToVehicleResponse(VehicleEntity vehicleEntity)
        {
            if (vehicleEntity == null)
            {
                return null;
            }

            return new VehicleResponse
            {
                Id = vehicleEntity.Id,
                Plaque = vehicleEntity.Plaque,
                PicturePath = vehicleEntity.PicturePath
            };
        }

        public ShippingDetailResponse ToShippingDetailResponse(ShippingDetailEntity shippingDetailEntity)
        {
            if (shippingDetailEntity == null)
            {
                return null;
            }

            return new ShippingDetailResponse
            {
                Id = shippingDetailEntity.Id,
                Quantity = shippingDetailEntity.Quantity,
                Description = shippingDetailEntity.Description,
                PicturePath = shippingDetailEntity.PicturePath
            };
        }

        public TripDetailResponse ToTripDetailResponse(TripDetailEntity tripDetailEntity)
        {
            if (tripDetailEntity == null)
            {
                return null;
            }

            return new TripDetailResponse
            {
                Date = tripDetailEntity.Date,
                Latitude = tripDetailEntity.Longitude,
                Longitude = tripDetailEntity.Longitude
            };
        }
    }
}



