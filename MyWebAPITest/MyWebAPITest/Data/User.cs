using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebAPITest.Data
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required] 
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(255)]
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }


    }
}
