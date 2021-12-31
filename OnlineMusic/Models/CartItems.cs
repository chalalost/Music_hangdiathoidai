using OnlineMusic.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineMusic.Models
{
    [Serializable]
    public class CartItems
    {
        public SANPHAM Product { get; set; }
        public int Quantity { get; set; }
    }
}