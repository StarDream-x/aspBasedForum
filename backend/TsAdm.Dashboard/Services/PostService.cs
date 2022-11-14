using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using TsAdm.Dashboard.RequestBodies;

namespace TsAdm.Dashboard.Services
{
    public class PostService
    {
        private MysqlService mysqlService = new MysqlService();

        public void postComment(long contentId, string text, string currentUserId)
        {
            using (MySqlConnection msc = mysqlService.newConnection())
            {
                msc.Open();
                string sql = $"insert into comment values (0, '{currentUserId}', '{text}', 0)";
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                cmd.ExecuteNonQuery();

                string sql2 = $"select id from comment where content = '{text}' and " +
                    $"user_id = '{currentUserId}'";
                MySqlCommand cmd2 = new MySqlCommand(sql2, msc);
                List<long> ids = new List<long>();
                using (MySqlDataReader reader = cmd2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ids.Add(reader.GetInt64(0));
                    }
                }

                foreach (long id in ids)
                {
                    string sql3 = $"insert into content_comment values({contentId}, {id})";
                    MySqlCommand cmd3 = new MySqlCommand(sql3, msc);
                    try
                    {
                        cmd3.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
    }
}