using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiDotNet.Models
{
    public class Product
    {
        public int  id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public double price { get; set; }
        [ForeignKey("Category")]
        public int? Catid { get; set; }

        public Category ?Category { get; set; }
    }
}
