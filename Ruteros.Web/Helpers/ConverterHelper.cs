﻿using Ruteros.Web.Data;
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
                    Quantity= sd.Quantity,
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
    }
}


     
