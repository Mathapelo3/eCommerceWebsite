
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace eCommerceWebsite.Models.SalesSubsystem
{
    public class Cart
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        [ValidateNever]
        public Product Product { get; set; }
        [ValidateNever]

        public string eCommerceUserId { get; set; }
        [ValidateNever]

        public eCommerceUser eCommerceUser { get; set; }

        public int Count { get; set; }

    }
}
