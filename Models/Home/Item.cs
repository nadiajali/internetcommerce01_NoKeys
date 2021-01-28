using internetcommerce01.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace internetcommerce01.Models.Home
{
    public class Item
    {
        public Tbl_Product Product { get; set; }
        public int Quantity { get; set; }
    }
}