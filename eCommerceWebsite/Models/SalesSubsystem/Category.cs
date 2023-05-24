using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eCommerceWebsite.Models.SalesSubsystem
{
    public class Category
    {
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        [DisplayName("Display Order")]

        public DateTime CreateDate { get; set; }
              = DateTime.Now;   

        public int DisplayOrder { get; set; }

        //public List<Item> Items { get; set; }
        //        = new List<Item>();
    }
}
