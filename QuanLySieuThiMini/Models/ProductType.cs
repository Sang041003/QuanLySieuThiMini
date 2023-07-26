
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace QuanLySieuThiMini.Models
{
    public class ProductType
    {

        public ProductType()
        {
        }
        [Key]
        public string typeID { get; set; }

        public string typeName { get; set; }

        public ICollection<Product> products { get; set; } = new List<Product>();

        public void getProType()
        {

        }

        public void addProType(ProductType type)
        {

        }

    }
}