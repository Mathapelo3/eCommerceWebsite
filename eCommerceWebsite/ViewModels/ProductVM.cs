﻿
using eCommerceWebsite.Models.SalesSubsystem;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace eCommerceWebsite.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; } = new Product();
        [ValidateNever]
        public IEnumerable<Product> Products { get; set; }
        =new List<Product>();
        [ValidateNever]
        public IEnumerable<SelectListItem> 
            Categories { get; set; }
    }
}
