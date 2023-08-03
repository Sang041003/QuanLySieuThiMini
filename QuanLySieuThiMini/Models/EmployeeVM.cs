namespace QuanLySieuThiMini.Models
{
    public class EmployeeVM
    {
        public int empID { get; set; }

        public string empName { get; set; }

        public string gender { get; set; }

        public int age { get; set; }

        public string empAddress { get; set; }

        public string? email { get; set; }
        public string? password { get; set; }
        public string empPhone { get; set; }
        public string posID { get; set; }
        public string? posName { get; set; }
    }
}
