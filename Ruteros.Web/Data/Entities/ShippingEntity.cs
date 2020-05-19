using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruteros.Web.Data.Entities
{
    public class ShippingEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public ICollection<ShippingDetailEntity> ShippingDetails { get; set; }
    }
}
