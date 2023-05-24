using eCommerceWebsite.Models.SalesSubsystem;

namespace eCommerceWebsite.IRepositories
{
    public interface ICategoryRepository : IRepository<Category>
    {

        void Update(Category category);

    }
}
