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
