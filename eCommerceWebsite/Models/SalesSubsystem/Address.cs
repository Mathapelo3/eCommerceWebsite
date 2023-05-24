namespace eCommerceWebsite.Models.SalesSubsystem
{
    public class Address
    {
        public Address()
        {
        


        }

        public Address( string street, string suburb, string city, string postalCode)
        {
           
            Street = street;
            Suburb = suburb;
            City = city;
            PostalCode = postalCode;
            
        }

        public int Id { get; set; }

        public string Street { get; set; }

        public string Suburb { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string PhoneNumber { get; set; }
    }
}
