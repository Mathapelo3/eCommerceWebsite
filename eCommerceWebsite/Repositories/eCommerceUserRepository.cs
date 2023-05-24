using eCommerceWebsite.Data;
using eCommerceWebsite.IRepositories;
using eCommerceWebsite.Models;

namespace eCommerceWebsite.Repositories
{
    public class eCommerceUserRepository : Repository<eCommerceUser>, IeCommerceUserRepository
    {
        private ApplicationDbContext _context;
        public eCommerceUserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


    }
}
