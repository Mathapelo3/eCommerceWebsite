using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eCommerceWebsite.Models.SalesSubsystem
{
    public class Product
    {
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public double Price { get; set; }
        [ValidateNever]

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        [ValidateNever]
        //public int ItemTypeId { get; set; }

        public virtual Category Category { get; set; }

        //public virtual ItemType ItemType { get; set; }
    }
}
    