using eCommerceWebsite.Models.SalesSubsystem;

namespace eCommerceWebsite.IRepositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        void Update(OrderDetail orderDetail);

    }
}
