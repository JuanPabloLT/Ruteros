using System;
using System.Collections.Generic;
using System.Text;

namespace Ruteros.Common.Models
{
    public class ShippingResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public List<ShippingDetailResponse> ShippingDetails { get; set; }
    }
}
