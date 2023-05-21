using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebAPITest.Data
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public Guid CategoryID { get; set; }
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
