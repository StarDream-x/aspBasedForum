using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using TsAdm.Dashboard.RequestBodies;

namespace TsAdm.Dashboard.Services
{
    public class PostService
    {
        private MysqlService mysqlService = new MysqlService();
        private ContentService contentService = new ContentService();

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

        public Content postContent(ContentPost post, string currentUserId)
        {
            using (MySqlConnection msc = mysqlService.newConnection())
            {
                msc.Open();
                string sql = $"insert into content values (0, '{post.title}', '{currentUserId}', " +
                    $"0, 0, 0, 0, '{post.body}')";
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                cmd.ExecuteNonQuery();

                string sql2 = $"select id from content where author_id='{currentUserId}' " +
                    $"order by id desc limit 1";
                MySqlCommand cmd2 = new MySqlCommand(sql2, msc);
                long id = (long) cmd2.ExecuteScalar();

                foreach (string mediaUrl in post.media)
                {
                    string sql3 = $"insert into content_media values ({id}, '{mediaUrl}')";
                    MySqlCommand cmd3 = new MySqlCommand(sql3, msc);
                    cmd3.ExecuteNonQuery();
                }

                return contentService.getContent(id, currentUserId);
            }
        }
    }
}