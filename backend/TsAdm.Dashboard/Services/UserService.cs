using MySql.Data.MySqlClient;
using System;
using TsAdm.Dashboard.RequestBodies;

namespace TsAdm.Dashboard.Services
{
    public class UserService
    {
        private MysqlService mysqlService;

        public UserInfo getUserById(string userId, string currentUserId)
        {
            try
            {
                using (MySqlConnection msc = mysqlService.newConnection())
                {
                    msc.Open();
                    string sql = $"select id, avatar_url, name, introduction, following_count, follower_count, favorite_count from user_info where id = '{userId}'";
                    MySqlCommand cmd = new MySqlCommand(sql, msc);


                    UserInfo userInfo = new UserInfo();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userInfo.id = reader.GetString(0);
                            userInfo.avatarUrl = reader.GetString(1);
                            userInfo.username = reader.GetString(2);
                            userInfo.introduction = reader.GetString(3);
                            userInfo.followingCount = reader.GetInt64(4);
                            userInfo.followerCount = reader.GetInt64(5);
                            userInfo.favoriteCount = reader.GetInt64(6);
                        }
                    }

                    string sql2 = $"select * from user_following where follower_id = {currentUserId}, and followee_id = {userId}";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, msc);
                    using (MySqlDataReader reader2 = cmd2.ExecuteReader())
                    {
                        userInfo.following = reader2.Read();
                    }

                    return userInfo;
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return null;
            }
        }

    
    }
}