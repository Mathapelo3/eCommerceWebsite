using eCommerceWebsite.Data;
using eCommerceWebsite.IRepositories;
using eCommerceWebsite.Models.SalesSubsystem;

namespace eCommerceWebsite.Repositories
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private ApplicationDbContext _context;
        public OrderDetailRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(OrderDetail orderDetail)
        {
            _context.OrderDetail.Update(orderDetail);
            //var categoryDB = _context.Categories.FirstOrDefault(x => x.Id == category.Id);
            //if (categoryDB != null)
            //{
            //    categoryDB.Name = category.Name;
            //    categoryDB.DisplayOrder = category.DisplayOrder;
            //}
        }
    }
}
