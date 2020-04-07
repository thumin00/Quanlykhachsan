using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace qlKhachSan
{
    class Thucthisql
    {
        public static SqlConnection conn;
        public static string conString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Integrated Security=True";
        public static void ketnoi()
        {
            conn = new SqlConnection();
            conn.ConnectionString = conString;
            if(conn.State!=ConnectionState.Open)
            {
                conn.Open();
            }
        }
        public static void Dongketnoi()
        {
            
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
        public static DataTable Docbang(string sql)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter mydata = new SqlDataAdapter();
            mydata.SelectCommand = new SqlCommand();
            ketnoi();
            mydata.SelectCommand.Connection = conn;
            mydata.SelectCommand.CommandText = sql;
            mydata.Fill(dt);
            Dongketnoi();
            return dt;
        }
        public static void capnhat(string sql)
        {
            ketnoi();
            SqlCommand sqlcomand = new SqlCommand();
            sqlcomand.Connection = conn;
            sqlcomand.CommandText = sql;
            sqlcomand.ExecuteNonQuery();
            Dongketnoi();
        }
    }
}
