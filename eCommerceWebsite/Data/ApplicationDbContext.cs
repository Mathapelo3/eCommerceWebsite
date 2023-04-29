using eCommerceWebsite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerceWebsite.Data
{
    public class ApplicationDbContext :IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<eCommerceLeadEntity> eCommerceLead {get;set;}
        public DbSet<eCommerceUser> eCommerceUser { get; set; }
    }
}
