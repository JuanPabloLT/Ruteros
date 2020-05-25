﻿using System;
using System.Collections.Generic;

namespace Ruteros.Common.Models
{
    public class TripResponse
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime StartDateLocal => StartDate.ToLocalTime();

        public DateTime? EndDate { get; set; }

        public DateTime? EndDateLocal => EndDate?.ToLocalTime();

        public string Source { get; set; }

        public string Target { get; set; }

        public double SourceLatitude { get; set; }

        public double SourceLongitude { get; set; }

        public double TargetLatitude { get; set; }

        public double TargetLongitude { get; set; }

        public string Remarks { get; set; }

        public List<TripDetailResponse> TripDetails { get; set; }

        public UserResponse User { get; set; }
        public VehicleResponse Vehicle { get; set; }
        public WarehouseResponse Warehouse { get; set; }
        public ShippingResponse Shipping { get; set; }
    }
}
