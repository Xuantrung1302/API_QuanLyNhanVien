using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_QuanLyNhanVien.Controllers
{
    public class QuanLyPhongBanController : ApiController
    {
        DBConnect DBConnect = new DBConnect();

        [HttpGet]
        [Route("api/QuanLyPhongBan/danhsachphongban")]
        public object LayPhongBan(string Id = null)
        {
            object result = new List<object>();
            DataTable dt = new DataTable();
            if (Id != null)
            {
                SqlParameter[] searchParams = {
                        new SqlParameter("@DepartmentID",Id)
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

    }
}
