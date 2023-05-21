using System.ComponentModel.DataAnnotations;

namespace MyWebAPITest.Models
{
    public class CategoryVM
    {
        public Guid CategoryID { get; set; }
        [StringLength(50)]
        public string CategoryName { get; set; }
    }
}
