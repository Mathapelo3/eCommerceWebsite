using Microsoft.AspNetCore.Identity;

namespace eCommerceWebsite.Models
{
    public class eCommerceUser:IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? PinCode { get; set; }


    }
}
