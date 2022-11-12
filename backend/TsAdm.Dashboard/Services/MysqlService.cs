﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using TsAdm.Dashboard.RequestBodies;

namespace TsAdm.Dashboard.Services
{
    public class MysqlService
    {
        
        public MySqlConnection openService()
        {
            //与数据库连接的信息
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            //用户名
            builder.UserID = "root";
            //密码
            builder.Password = "2020302";
            //服务器地址
            builder.Server = "localhost";
            //连接时的数据库
            builder.Database = "dotnetFinal";
            //定义与数据连接的链接
            MySqlConnection msc = new MySqlConnection(builder.ConnectionString);
            return msc;
        }
        
        public void test()
        {
            MySqlConnection msc = openService();

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

        public bool login(Body body)
        {
            bool result = false;
            try
            {
                MySqlConnection msc = openService();


                string sql = "select * from user where username = '" + body.username + "' and password = '" + body.password + "'";
                //创建命令对象
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                //打开数据库连接
                msc.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    return true;
                }

                reader.Close();
                msc.Close();
            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }
            return result;
        }

        public bool checkId(Query query)
        {
            bool result = false;
            try
            {
                MySqlConnection msc = openService();


                string sql = "select * from user where id = '" + query.id ;
                //创建命令对象
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                //打开数据库连接
                msc.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
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

        public bool Content(Body body)
        {
            bool result = false;
            try
            {
                MySqlConnection msc = openService();


                string sql = "select * from content where title = '" + body.title + "' and body = '" + body.body + "'and media = '"+body.media+"'";
                //创建命令对象
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                //打开数据库连接
                msc.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
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