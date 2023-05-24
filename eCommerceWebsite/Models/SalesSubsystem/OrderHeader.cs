using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace eCommerceWebsite.Models.SalesSubsystem
{
    public class OrderHeader
    {
        public int Id { get; set; }

        public string eCommerceUserId { get; set; }
        [ValidateNever]

        public eCommerceUser eCommerceUser { get; set; }
        [Required]

        public DateTime DateOfOrder { get; set; }

        public DateTime DateOfShipping { get; set; }

        public double OrderTotal { get; set; }

        public string? OrderStatus { get; set; }

        public string? PaymentStatus { get; set; }

        public string? TrackingNumber { get; set; }

        public string? Carrier { get; set; }

        public string? SessionId { get; set; }

        public string? PaymentIntentId { get; set; }

        public DateTime DateOfPayment { get; set; }

        public DateTime DueDate { get; set; }
        [Required]

        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Name { get; set; }


    }
}
