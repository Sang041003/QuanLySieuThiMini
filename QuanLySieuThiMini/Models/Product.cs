
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace QuanLySieuThiMini.Models
{
    public class Product
    {

        public Product()
        {
        }
        [Key]
        public int proID { get; set; }

        public string proName { get; set; }
        public string typeID { get; set; }
        public ProductType type { get; set; }

        public int cost { get; set; }

        public int inventory { get; set; }

        public int shelfID { get; set; }


        public ICollection<BillDetail> bill { get; set; } = new List<BillDetail>();

        public int getProductID()
        {
            return 0;
        }

        public List<Product> getProInfo()
        {
            return null;
        }

        public List<Product> getProInfoByType(string typeID)
        {
            return null;
        }

        public List<Product> searchProInfo(string keyword)
        {
            return null;
        }

        public int getInventory()
        {
            return 0;
        }

    }
}