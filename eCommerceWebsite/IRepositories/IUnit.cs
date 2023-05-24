namespace eCommerceWebsite.IRepositories
{
    public interface IUnit
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ICartRepository Cart { get; }

        IeCommerceUserRepository eCommerceUser { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailRepository OrderDetail { get; }

        void Save();

    }
}
