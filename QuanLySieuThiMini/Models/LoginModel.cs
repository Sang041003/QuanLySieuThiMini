using System.ComponentModel.DataAnnotations;

namespace QuanLySieuThiMini.Models
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
