using System.ComponentModel.DataAnnotations;

namespace MyWebAPITest.Models
{
    public class BookModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(2500)]
        public string Description { get; set; }
        [Required]
        [StringLength(100)]
        public string Author { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double Prices { get; set; }
        public byte Discount { get; set; }
        public Guid? CateID { get; set; }
    }
}
