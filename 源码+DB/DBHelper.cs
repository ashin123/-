using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamtasiaStudio
{
    class DBHelper
    {
        //数据库连接字符串
        public static string ConnString = "server=.;database=CamtasiaStudioDB;Integrated Security=True";
        //数据库连接对象
        private static SqlConnection conn = null;
        //初始化数据连接
        private static void InitConnection()
        {
            //如果连接对象不存在，则创建连接
            if (conn == null)
            {
                conn = new SqlConnection(ConnString);
            }
            //如果连接对象关闭，则打开连接
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            //如果连接中断，则重启连接
            if (conn.State == ConnectionState.Broken)
            {
                conn.Close();
                conn.Open();
            }
        }

        //查询，获取DataReader
        public static SqlDataReader GetDataReader(string sqlStr)
        {
            InitConnection();
            SqlCommand cmd = new SqlCommand(sqlStr, conn);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        //查询，获取DataTable
        public static DataTable GetDataTable(string sqlStr)
        {
            InitConnection();
            DataTable table = new DataTable();
            SqlDataAdapter dap = new SqlDataAdapter(sqlStr, conn);
            dap.Fill(table);
            conn.Close();
            return table;
        }

        //增删改
        public static bool ExecuteNonQuery(string sqlStr)
        {
            InitConnection();
            SqlCommand cmd = new SqlCommand(sqlStr, conn);
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result > 0;
        }

        //执行聚合函数
        public static object ExecuteScalar(string sqlStr)
        {
            InitConnection();
            SqlCommand cmd = new SqlCommand(sqlStr, conn);
            object result = cmd.ExecuteScalar();
            conn.Close();
            return result;
        }
    }
}
