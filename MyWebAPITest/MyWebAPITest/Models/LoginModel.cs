using System.ComponentModel.DataAnnotations;

namespace MyWebAPITest.Models
{
    public class LoginModel
    {
        [Required]
        public string UserNameOrEmail { get; set; }
        [Required]
        [StringLength(255)]
        public string Password { get; set; }
    }
}
