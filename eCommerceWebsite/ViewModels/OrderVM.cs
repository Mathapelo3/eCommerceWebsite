using eCommerceWebsite.Models.SalesSubsystem;

namespace eCommerceWebsite.ViewModels
{
    public class OrderVM
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
    }
}
