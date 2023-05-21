using MyWebAPITest.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebAPITest.Data
{
    public enum StatusOrder
    {
        New=0, Payment=1, Complete=2, Cancel = -1
    }
    [Table("Orders")]
    public class Order
    {
        
        public Guid OrderID { get; set; }
        [Required]
        [MyDate(ErrorMessage ="Invalid day, Day must >= today")]
        public DateTime OrderDay { get; set; }
        public DateTime OrderDivery { get; set; }
        public StatusOrder OrderStatus { get; set; }
        public string ReceiverName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
