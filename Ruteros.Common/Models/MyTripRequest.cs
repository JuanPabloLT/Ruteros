using System;

namespace Ruteros.Common.Models
{
    public class MyTripsRequest
    {
        public string UserId { get; set; }

        public DateTime StartDate { get; set; }

        //public DateTime EndDate { get; set; }
        public string Document { get; set; }
        public string Shipping { get; set; }
    }
}
