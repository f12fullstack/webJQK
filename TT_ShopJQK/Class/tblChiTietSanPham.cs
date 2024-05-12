using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TT_ShopJQK.Class
{
    public class detailProduct
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string NameCategoryId { get; set; }
        public decimal Price { get; set; }
        public decimal LastPrice { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public List<string> Url { get; set; }

        public List<(string Url, int Id)> Images { get; set; }

    }
}