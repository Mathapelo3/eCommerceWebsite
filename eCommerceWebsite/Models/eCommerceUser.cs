using Microsoft.AspNetCore.Identity;

namespace eCommerceWebsite.Models
{
    public class eCommerceUser:IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
