using System.ComponentModel.DataAnnotations;

namespace Ruteros.Common.Models
{
    public class TripDetailRequest
    {
        [Required]
        public int TripId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
