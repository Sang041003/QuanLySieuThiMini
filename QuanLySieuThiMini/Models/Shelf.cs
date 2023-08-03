using System.ComponentModel.DataAnnotations;

namespace QuanLySieuThiMini.Models
{
    public class Shelf
    {
        public Shelf() { }
        [Key]
        public int shelfID { get; set; }

        public string shelfLocation { get; set; }

        public ICollection<Product> products { get; set; } = new List<Product>();
    }
}
