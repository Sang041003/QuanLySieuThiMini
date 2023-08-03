using System.ComponentModel.DataAnnotations;

namespace QuanLySieuThiMini.Models
{
    public class Position
    {
        [Key]
        public string posID { get; set; }
        public string posName { get; set; }
        public ICollection<Employee> employees { get; set; } = new List<Employee>();
    }
}
