
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace QuanLySieuThiMini.Models
{
    public class Employee
    {
        public Employee()
        {
        }
        [Key]
        public int empID { get; set; }

        public string empName { get; set; }

        public string gender { get; set; }

        public int age { get; set; }

        public string empAddress { get; set; }

        public string empPhone { get; set; }

        public string email { get; set; }


        [ForeignKey("Position")]
        public string posID { get; set; }
        public Position Position { get; set; }

    }
}