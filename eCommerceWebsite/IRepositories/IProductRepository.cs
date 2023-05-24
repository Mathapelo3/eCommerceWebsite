using eCommerceWebsite.Models.SalesSubsystem;

namespace eCommerceWebsite.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}
