
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuanLySieuThiMini.Models
{
    public class User
    {

        public User()
        {
        }

        public int userID { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string role { get; set; }

        public void changePassword(string newPass)
        {

        }

        public bool login(string email, string password)
        {

            return false;
        }

    }
}