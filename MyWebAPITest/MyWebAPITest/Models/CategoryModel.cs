using System.ComponentModel.DataAnnotations;

namespace MyWebAPITest.Models
{
    public class CategoryModel
    {
        [StringLength(50)]
        public string CategoryName { get; set; }
    }
}
