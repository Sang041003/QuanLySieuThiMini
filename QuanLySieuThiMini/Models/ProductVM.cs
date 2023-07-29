namespace QuanLySieuThiMini.Models
{
    public class ProductVM
    {
        public int proID { get; set; }

        public string proName { get; set; }
        public string typeID { get; set; }
        public Type? type { get; set; }
        public string? typeName { get; set; }

        public int cost { get; set; }

        public int inventory { get; set; }

        public int shelfID { get; set; }
    }
}
