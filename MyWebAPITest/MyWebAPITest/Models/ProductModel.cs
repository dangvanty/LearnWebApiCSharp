namespace MyWebAPITest.Models
{
    public class ProductModelVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
    public class ProductModel : ProductModelVM
    {
        public Guid ProductId { get; set; }  
    }
}
