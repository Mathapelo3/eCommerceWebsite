using eCommerceWebsite.Models.SalesSubsystem;

namespace eCommerceWebsite.IRepositories
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);
        void UpdateStatus(int Id, string orderStatus, string? paymentStatus = null);
        void PaymentStatus(int Id, string SessionId, string PaymentIntentId);
    }
}
