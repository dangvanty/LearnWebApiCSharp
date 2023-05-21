using System.ComponentModel.DataAnnotations;

namespace MyWebAPITest.Models
{
    public class BookModel
    {
        public Guid Id { get; set; }
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
    public class BookUpdate
    {

        public Guid Id { get; set; }
        [StringLength(100)]
        public string? Name { get; set; }
        [StringLength(100)]
        public string? Title { get; set; }
        [StringLength(2500)]
        public string? Description { get; set; }
        [StringLength(100)]
        public string? Author { get; set; }
        [Range(0, double.MaxValue)]
        public double? Prices { get; set; }
        public byte? Discount { get; set; }
        public Guid? CateID { get; set; }

    }
    public class BookVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public double Prices { get; set; }
        public byte Discount { get; set; }
        public Guid? CateID { get; set; }
        public string CategoryName {get; set;}
    }
}
