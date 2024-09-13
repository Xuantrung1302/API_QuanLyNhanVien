using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_QuanLyNhanVien.Models
{
    public class EmployeesModel
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public int DepartmentID { get; set; }
    }
}