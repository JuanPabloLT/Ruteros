using System;
using System.Collections.Generic;
using System.Text;

namespace Ruteros.Common.Models
{
    public class ShippingDetailResponse
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public string PicturePath { get; set; }

        public string PictureFullPath => string.IsNullOrEmpty(PicturePath)
        ? "https://ruteroswebitm.azurewebsites.net//images/noimage.png"
        : $"https://ruteroswebitm.azurewebsites.net{PicturePath.Substring(1)}";
    }
}
