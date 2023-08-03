
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace QuanLySieuThiMini.Models
{
    public class Guest
    {
        public Guest()
        {
        }

        [Key]
        public string guestPhone { get; set; }

        public string guestName { get; set; }

        public ICollection<Bill> Bills { get; set; } = new List<Bill>();
    }
}