using eCommerceWebsite.Models.SalesSubsystem;

namespace eCommerceWebsite.IRepositories
{
    public interface ICartRepository : IRepository<Cart>
    {

        int IncrementCartItem(Cart cart, int count);
        int DecrementCartItem(Cart cart, int count);
    }
}
