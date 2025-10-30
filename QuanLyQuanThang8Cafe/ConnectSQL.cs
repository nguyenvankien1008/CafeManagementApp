using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace QuanLyQuanThang8Cafe
{
    public class ConnectSQL
    {
           private static MySqlConnection GetConnection()
           {
                string connStr = @"Server=localhost;Port=3306;Database=QuanLyCafe;Uid=root;Pwd=123456;";
                return new MySqlConnection(connStr);
           }

        private static MySqlConnection cnn;

        public static void OpenConnection()
        {
            cnn = GetConnection();
            cnn.Open();
        }

        public static void CloseConnection() 
        {
            if (cnn != null && cnn.State == ConnectionState.Open)
            {
                cnn.Close();
            }

        }

        // Hàm chạy lệnh Sql lấy dữ liệu Data Query

        public static DataTable ThucThiQuery(string sql) 
        {
            OpenConnection();
            MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = sql;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // Hàm chạy lệnh Sql thêm, xóa, sửa Non Query

        public static string ThucThiNonQuery(string sql) 
        {
            OpenConnection();
            MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            CloseConnection();
            return "Success";
        }

        // Phương thức kiểm tra sự tồn tại của dữ liệu
        public static bool ExecuteReader_bool(string sql)
        {
            OpenConnection();
            MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = sql;
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
                return true;
            }
            else
            {
                dr.Close();
                return false;
            }
        }

        // Phương thức trả về 1 giá trị nào đó mà ta tìm
        public static string ExecuteScalar_string(string sql)
        {
            OpenConnection();
            MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = sql;
            return cmd.ExecuteScalar().ToString();
        }

