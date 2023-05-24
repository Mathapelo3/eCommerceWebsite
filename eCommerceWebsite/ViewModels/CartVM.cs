using eCommerceWebsite.Models.SalesSubsystem;

namespace eCommerceWebsite.ViewModels
{
    public class CartVM
    {
        public IEnumerable<Cart> ListOfCart { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
