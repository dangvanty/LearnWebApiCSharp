namespace MyWebAPITest.Data
{
    public class OrderDetail
    {
        public Guid OrderID { get; set; }
        public Guid BookID {get;set;}
        public int Quantity { get; set;}
        public double Price { get; set;}
        public byte Discount { get; set;}
        
        public Order order { get; set;}
        public Book book { get; set;}
    }
}
