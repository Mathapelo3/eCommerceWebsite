using eCommerceWebsite.Models;
using eCommerceWebsite.Models.SalesSubsystem;
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

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }

        //public DbSet<ItemType> ItemTypes { get; set; }

        //public DbSet<OrderDetails> Orders { get; set; }

        //public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Cart> Carts { get; set; }

        //public DbSet<CartItem> CartItems { get; set; }

        //public DbSet<Address> Address { get; set; }

        //public DbSet<PaymentDetails> PaymentDetails { get; set; }

    }
}
