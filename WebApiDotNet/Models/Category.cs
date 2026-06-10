namespace WebApiDotNet.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string name { get; set; }

        public List<Product>? products { get; set; }
    }
}
