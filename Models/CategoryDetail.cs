using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace internetcommerce01.Models
{
    public class CategoryDetail
    {
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "Category Name Is Required")]
        [StringLength(100, ErrorMessage = "Minimum 3 and Maximum 100 Characters Are Allowed", MinimumLength = 3)]
        public string CategoryName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }

    public class ProductDetail
    {
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Product Name Is Required")]
        [StringLength(100, ErrorMessage = "Minimum 3 and Maximum 100 Characters Are Allowed", MinimumLength = 3)]
        public string ProductName { get; set; }
        [Required]
        [Range(1, 50)]
        public Nullable<int> CategoryID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [Required(ErrorMessage = "Description Is Required")]
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public Nullable<bool> IsFeatured { get; set; }
        [Required]
        [Range(typeof(int), "1", "500", ErrorMessage = "Invalid Quantity")]
        public Nullable<int> Quantity { get; set; }
        [Required]
        [Range(typeof(int), "1", "200000", ErrorMessage = "Invalid Price")]
        public Nullable<decimal> Price { get; set; }
        public SelectList Categories { get; set; }
    }
}