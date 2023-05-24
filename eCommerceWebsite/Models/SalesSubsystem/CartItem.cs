using System.Text.Json.Serialization;

namespace eCommerceWebsite.Models.SalesSubsystem
{
    public class CartItem
    {

        public CartItem(int itemId, int quantity, decimal unitPrice) 
        {
        
            ItemId = itemId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
        public int Id { get; set; }

        public Guid CartId { get; set; }

        public int ItemId { get; set; } 

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
        //[JsonIgnore]

        //public Cart Cart { get; set; }
    }
}
