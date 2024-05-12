using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TT_ShopJQK.Class
{
    public class HoaDon
    {
        public int OrderID { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Status { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set;}
    }
}