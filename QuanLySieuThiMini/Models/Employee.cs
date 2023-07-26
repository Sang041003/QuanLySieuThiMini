
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public List<Employee> getEmployeeInfo()
        {
            return null;
        }

        public void addEmployee(Employee emp)
        {
        }

        public bool removeEmployee(int empID)
        {
            return false;
        }

    }
}