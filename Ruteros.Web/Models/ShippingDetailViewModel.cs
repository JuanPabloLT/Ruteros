using Microsoft.AspNetCore.Http;
using Ruteros.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ruteros.Web.Models
{
    public class ShippingDetailViewModel : ShippingDetailEntity
    {
        public int ShippingId { get; set; }

        [Display(Name = "Picture")]
        public IFormFile PictureFile { get; set; }
    }
}
