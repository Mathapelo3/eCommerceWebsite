using eCommerceWebsite.Data;
using eCommerceWebsite.IRepositories;

namespace eCommerceWebsite.Repositories
{
    public class Unit: IUnit
    {
        private ApplicationDbContext _context;
        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }
        public ICartRepository Cart { get; private set; }

        public IeCommerceUserRepository eCommerceUser { get; private set; }

        public IOrderHeaderRepository OrderHeader { get; private set; }

        public IOrderDetailRepository OrderDetail { get; private set; }


        public Unit(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(context);
            Product = new ProductRepository(context);
            Cart = new CartRepository(context);
            eCommerceUser = new eCommerceUserRepository(context);
            OrderDetail = new OrderDetailRepository(context);
            OrderHeader = new OrderHeaderRepository(context);
        }

       
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
