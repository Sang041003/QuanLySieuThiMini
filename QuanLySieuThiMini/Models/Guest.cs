
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

        public string guestName { get; set; }
        [Key]
        public string guestPhone { get; set; }

        public ICollection<Bill> bill { get; set; } = new List<Bill>();

        public List<Guest> getGuestInfo()
        {
            return null;
        }

    }
}