using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Security.Principal;

namespace API_QuanLyNhanVien.Controllers
{
    public class QuanLyNhanVienController : ApiController
    {
        DBConnect DBConnect = new DBConnect();

        [HttpGet]
        [Route("api/QuanLyNhanVien/danhsachnhanvien")]
        public object XuatNhanVien(string IdE = null, string employeeName = null)
        {
            object result = new List<object>();
            DataTable dt = new DataTable();
            if (IdE != null || employeeName != null)
            {
                SqlParameter[] searchParams = {
                        new SqlParameter("@EmployeeID",IdE),
                        new SqlParameter("@EmployeeName",employeeName)
                };
                dt = DBConnect.ExecuteQuery("SP_SELECT_SEARCH_EMPLOYEES", searchParams);
            }
            else
            {
                dt = DBConnect.ExecuteQuery("SP_SELECT_SEARCH_EMPLOYEES");
            }

            if (dt.Rows.Count > 0)
                result = dt;
            return result;
        }

        [HttpPost]
        [Route("api/QuanLyNhanVien/themnhanvien")]
        public bool ThemNhanVien([FromBody] JObject data)
        {
            bool result = false;
            string firstName = data["FirstName"].ToString();
            string lastName = data["LastName"].ToString();
            string dob = data["DOB"].ToString();
            string gender = data["Gender"].ToString();
            string address = data["Address"].ToString();
            string position = data["Position"].ToString();
            int departmentId = (int)data["DepartmentID"];

            SqlParameter[] insertparams = {
                    new SqlParameter("@FirstName", firstName),
                    new SqlParameter("@LastName", lastName),
                     new SqlParameter("@DOB", dob),
                      new SqlParameter("@Gender", gender),
                    new SqlParameter("@Address", address),
                     new SqlParameter("@Position", position),
                    new SqlParameter("@DepartmentID", departmentId)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_EMPLOYEES", insertparams);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        [HttpPost]
        [Route("api/QuanLyNhanVien/suanhanvien")]
        public bool SuanhanVien([FromBody] JObject data)
        {
            bool result = false;
            string employeeID = data["EmployeeID"].ToString();
            string firstName = data["FirstName"].ToString();
            string lastName = data["LastName"].ToString();
            string dob = data["DOB"].ToString();
            string gender = data["Gender"].ToString();
            string address = data["Address"].ToString();
            string position = data["Position"].ToString();
            int departmentId = (int)data["DepartmentID"];

            SqlParameter[] updateparams = {
                    new SqlParameter("@EmployeeID", employeeID),
                    new SqlParameter("@FirstName", firstName),
                    new SqlParameter("@LastName", lastName),
                     new SqlParameter("@DOB", dob),
                      new SqlParameter("@Gender", gender),
                    new SqlParameter("@Address", address),
                     new SqlParameter("@Position", position),
                    new SqlParameter("@DepartmentID", departmentId)
            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_EMPLOYEES", updateparams);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/QuanLyNhanVien/xoanhanvien")]
        public bool XoaNhanVien(string Id)
        {

            bool result = false;
            if (Id != null)
            {
                SqlParameter[] deleteParams = {
                    new SqlParameter("@EmployeeID", Id),
                    };

                result = DBConnect.ExecuteNonQuery("SP_DELETE_EMPLOYEES", deleteParams);

            }
            return result;
        }

        //API Phòng Ban
        [HttpGet]
        [Route("api/QuanLyNhanVien/danhsachphongban")]
        public object XuatPhongBan(int? IdD = null)
        {
            object result = new List<object>();
            DataTable dt = new DataTable();
            if (IdD != null)
            {
                SqlParameter[] searchParams = {
                        new SqlParameter("@DepartmentID",IdD)

                };
                dt = DBConnect.ExecuteQuery("SP_SELECT_SEARCH_DEPARTMENT", searchParams);
            }
            else
            {
                dt = DBConnect.ExecuteQuery("SP_SELECT_SEARCH_DEPARTMENT");
            }

            if (dt.Rows.Count > 0)
                result = dt;
            return result;
        }
        [HttpPost]
        [Route("api/QuanLyNhanVien/themphongban")]
        public bool ThemPhongBan([FromBody] JObject data)
        {
            bool result = false;
            string department = data["DepartmentName"].ToString();

            SqlParameter[] insertparams = {
                    new SqlParameter("@DepartmentName", department),

            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_DEPARTMENT", insertparams);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        [HttpPost]
        [Route("api/QuanLyNhanVien/suaphongban")]
        public bool SuaPhongBan([FromBody] JObject data)
        {
            bool result = false;
            string departmentid = data["DepartmentID"].ToString();
            string department = data["DepartmentName"].ToString();


            SqlParameter[] updateparams = {
                    new SqlParameter("@DepartmentID", departmentid),
                    new SqlParameter("@DepartmentName", department),

            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_DEPARTMENT", updateparams);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        [Route("api/QuanLyNhanVien/xoaphongban")]
        public int XoaPhongBan(string Id)
        {
            int result = 0;
            if (Id != null)
            {
                SqlParameter[] deleteParams = {
                    new SqlParameter("@DepartmentID", Id),
                    };

                result = DBConnect.ExecuteDeleteProc("SP_DELETE_DEPARTMENT", deleteParams);

            }
            return result;
        }
        //API UserAccount
        [HttpGet]
        [Route("api/QuanLyNhanVien/danhsachtaikhoan")]
        public object XuatTaiKhoan(string userName, string passWord)
        {
            object result = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] searchParams = {
                    new SqlParameter("@UserName",userName),
                    new SqlParameter("@Password",passWord)
            };

            dt = DBConnect.ExecuteQuery("[SP_SELECT_SEARCH_USERACCOUNTS]", searchParams);

            if (dt?.Rows?.Count > 0)
            {
                result = dt;
                return result;
            }

            return result;
        }
        [HttpPost]
        [Route("api/QuanLyNhanVien/suataikhoan")]
        public bool SuaTaiKhoan([FromBody] JObject data)
        {
            bool result = false;
            string username = data["Username"].ToString();
            string displayName = data["DisplayName"].ToString();
            string passwordHash = data["PasswordHash"].ToString();


            SqlParameter[] updateparams = {
                    new SqlParameter("@Username", username),
                    new SqlParameter("@DisplayName", displayName),
                    new SqlParameter("@PasswordHash", passwordHash),
            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_USERACCOUNTS", updateparams);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
