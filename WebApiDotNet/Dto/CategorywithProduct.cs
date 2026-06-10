namespace WebApiDotNet.Dto
{
    public class CategorywithProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int productcount { get; set; }

        public List<string> products { get; set; }
    }
}
