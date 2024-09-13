using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace API_QuanLyNhanVien
{
    public class DBConnect
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString;


        public DBConnect()
        {

        }

        // Thực thi stored procedure mà không trả về dữ liệu
        public bool ExecuteNonQuery(string procedureName, SqlParameter[] parameters)
        {
            bool result = false;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddRange(parameters);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        // Kiểm tra nếu có ít nhất 1 hàng bị ảnh hưởng thì trả về true
                        if (rowsAffected > 0)
                        {
                            result = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }
            return result;
        }

        // Thực thi stored procedure và trả về dữ liệu
        public DataTable ExecuteQuery(string procedureName, SqlParameter[] parameters = null)
        {
            var dataTable = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        public int ExecuteDeleteProc(string procedureName, SqlParameter[] parameters = null)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_DELETE_DEPARTMENT", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(resultParam);

                    // Mở kết nối và thực thi stored procedure
                    connection.Open();
                    command.ExecuteNonQuery();

                    // Lấy kết quả trả về
                    result = (int)resultParam.Value;
                }
            }
            return result;
        }
    }
}