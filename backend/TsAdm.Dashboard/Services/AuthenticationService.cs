using MySql.Data.MySqlClient;
using System;
using TsAdm.Dashboard.RequestBodies;

namespace TsAdm.Dashboard.Services
{
    public class AuthenticationService
    {
        private MysqlService mysqlService;

        public bool register(RegisterBody body)
        {
            try
            {
                using (MySqlConnection msc = mysqlService.newConnection())
                {
                    msc.Open();
                    string sql = "select * from user_info where id = '" + body.id + "'";
                    //创建命令对象
                    MySqlCommand cmd = new MySqlCommand(sql, msc);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return false;
                        }
                    }

                    string sql2 = "insert into user_info values('" + body.id +
                        "', '" + body.username + "', '" + body.password + "', '', '', 0, 0, 0)";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, msc);
                    cmd2.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return false;
            }
        }

        public bool login(LoginBody loginBody)
        {
            try
            {
                using (MySqlConnection msc = mysqlService.newConnection())
                {
                    msc.Open();
                    string sql = $"select * from user_info where id = '{loginBody.Id}' and password = '{loginBody.Password}'";
                    //创建命令对象
                    MySqlCommand cmd = new MySqlCommand(sql, msc);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
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
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return false;
            }
        }

        public bool checkId(Query query)
        {
            bool result = false;
            try
            {
                MySqlConnection msc = mysqlService.newConnection();


                string sql = "select * from user where id = '" + query.id + "'";
                //创建命令对象
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                //打开数据库连接
                msc.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    reader.Close();
                    return true;
                }

                reader.Close();
                msc.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return result;
        }
    }
}

