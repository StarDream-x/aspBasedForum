using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using TsAdm.Dashboard.RequestBodies;

namespace TsAdm.Dashboard.Services
{
    public class MysqlService
    {
        
        public MySqlConnection newConnection()
        {
            //与数据库连接的信息
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            //用户名
            builder.UserID = "root";
            //密码
            builder.Password = "123456";// "dummy";
            //服务器地址
            builder.Server = "localhost";
            //连接时的数据库
            builder.Database = "dotnetfinal";
            //定义与数据连接的链接
            MySqlConnection msc = new MySqlConnection(builder.ConnectionString);
            return msc;
        }
        
        public void test()
        {
            MySqlConnection msc = newConnection();

            string sql = "select * from user";
            //创建命令对象
            MySqlCommand cmd = new MySqlCommand(sql, msc);
            //打开数据库连接
            msc.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("[0]=" + reader[0] + "  [1]=" + reader[1] + "  [2]=" + reader[2]);
            }

            reader.Close();
            msc.Close();

        }
    }
}