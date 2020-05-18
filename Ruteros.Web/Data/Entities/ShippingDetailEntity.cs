using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruteros.Web.Data.Entities
{
    public class ShippingDetailEntity
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string PicturePath { get; set; }
        public ShippingEntity Shipping { get; set; }
    }
}
