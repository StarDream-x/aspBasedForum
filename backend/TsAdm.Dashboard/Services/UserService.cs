using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using TsAdm.Dashboard.RequestBodies;

namespace TsAdm.Dashboard.Services
{
    public class UserService
    {
        private MysqlService mysqlService = new MysqlService();

        public UserInfo getUserById(string userId, string currentUserId)
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

                string sql2 = $"select * from user_following where follower_id = '{currentUserId}' and followee_id = '{userId}'";
                MySqlCommand cmd2 = new MySqlCommand(sql2, msc);
                using (MySqlDataReader reader2 = cmd2.ExecuteReader())
                {
                    userInfo.following = reader2.Read();
                }

                return userInfo;
            }
        }

        //获取用户关注列表
        public List<ContentAbstract> getUserfavorite(string currentUserId)
        {
            using (MySqlConnection msc = mysqlService.newConnection())
            {
                msc.Open();
                string sql = $"select content.id, content.title,user_info.id, " +
                    $"user_info.name, content.view_count " +
                    $"from content, user_info, content_user_interaction " +
                    $"where content.author_id = user_info.id and " +
                    $"content_user_interaction.content_id = content.id and " +
                    $"content_user_interaction.user_id = '{currentUserId}' and " +
                    $"content_user_interaction.favorite = true";
                //创建命令对象
                MySqlCommand cmd = new MySqlCommand(sql, msc);

                List<ContentAbstract> contentAbstracts = new List<ContentAbstract>();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ContentAbstract contentAbstract = new ContentAbstract();
                        contentAbstract.id = reader.GetInt64(0);
                        contentAbstract.title = reader.GetString(1);
                        contentAbstract.authorId = reader.GetString(2);
                        contentAbstract.authorName = reader.GetString(3);
                        contentAbstract.viewCount = reader.GetInt64(4);
                        contentAbstracts.Add(contentAbstract);
                    }
                }

                foreach (ContentAbstract contentAbstract in contentAbstracts)
                {
                    long contentId = contentAbstract.id;
                    string sql2 = $"select media_url from content_media " +
                        $"where content_id = {contentId} " +
                        $"limit 1";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, msc);
                    using (MySqlDataReader reader2 = cmd2.ExecuteReader())
                    {
                        if (reader2.Read())
                        {
                            contentAbstract.imageUrl = reader2.GetString(0);
                        }
                    }
                }

                return contentAbstracts;
            }
        }

        //获取用户粉丝列表
        public List<UserInfo> getUserFollwer(string currentUserId)
        {
            using (MySqlConnection msc = mysqlService.newConnection())
            {
                msc.Open();
                string sql = $"select id, avatar_url, name, introduction, following_count, follower_count, favorite_count " +
                    $"from user_info, user_following " +
                    $" where user_info.id = user_following.follower_id and " +
                    $"user_following.followee_id = '{currentUserId}'";
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                List<UserInfo> userInfolist = new List<UserInfo>();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UserInfo userInfo = new UserInfo();
                        userInfo.id = reader.GetString(0);
                        userInfo.avatarUrl = reader.GetString(1);
                        userInfo.username = reader.GetString(2);
                        userInfo.introduction = reader.GetString(3);
                        userInfo.followingCount = reader.GetInt64(4);
                        userInfo.followerCount = reader.GetInt64(5);
                        userInfo.favoriteCount = reader.GetInt64(6);
                        userInfolist.Add(userInfo);
                    }
                }
                foreach(UserInfo userInfo in userInfolist)
                {
                    string sql2 = $"select * from user_following where follower_id = '{currentUserId}' and followee_id = '{userInfo.id}'";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, msc);
                    using (MySqlDataReader reader2 = cmd2.ExecuteReader())
                    {
                        userInfo.following = reader2.Read();
                    }
                }


                return userInfolist;
            }
        }

        //获取用户关注列表
        public List<UserInfo> getUserFollwee(string currentUserId)
        {
            using (MySqlConnection msc = mysqlService.newConnection())
            {
                msc.Open();
                string sql = $"select id, avatar_url, name, introduction, following_count, follower_count, favorite_count " +
                    $"from user_info, user_following " +
                    $" where user_info.id = user_following.followee_id and " +
                    $"user_following.follower_id = '{currentUserId}'";
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                List<UserInfo> userInfolist = new List<UserInfo>();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UserInfo userInfo = new UserInfo();
                        userInfo.id = reader.GetString(0);
                        userInfo.avatarUrl = reader.GetString(1);
                        userInfo.username = reader.GetString(2);
                        userInfo.introduction = reader.GetString(3);
                        userInfo.followingCount = reader.GetInt64(4);
                        userInfo.followerCount = reader.GetInt64(5);
                        userInfo.favoriteCount = reader.GetInt64(6);
                        userInfo.following = true;
                        userInfolist.Add(userInfo);
                    }
                }
                return userInfolist;
            }
        }

        //关注,取消关注  同样问题
        public bool userFollow(string currentUserId, string target_id, bool following)
        {
            using (MySqlConnection msc = mysqlService.newConnection())
            {
                msc.Open();
                string sql2 = $"select * from user_following where follower_id = '{currentUserId}' and " +
                    $"followee_id = '{target_id}'";
                MySqlCommand cmd2 = new MySqlCommand(sql2, msc);
                bool exist;
                using (MySqlDataReader mySqlDataReader = cmd2.ExecuteReader())
                {
                    exist = mySqlDataReader.Read();
                }

                if (!following && exist)
                {
                    string sql = $"delete from user_following where follower_id = '{currentUserId}' and " +
                        $"followee_id = '{target_id}'";
                    MySqlCommand cmd = new MySqlCommand(sql, msc);
                    cmd.ExecuteNonQuery();
                    string sql6 = $"update user_info set following_count=following_count-1 where id='{currentUserId}'";
                    MySqlCommand cmd6 = new MySqlCommand(sql6, msc);
                    cmd6.ExecuteNonQuery();
                    string sql7 = $"update user_info set follower_count=follower_count-1 where id='{target_id}'";
                    MySqlCommand cmd7 = new MySqlCommand(sql7, msc);
                    cmd7.ExecuteNonQuery();
                }
                else if (following && !exist)
                {
                    string sql3 = $"insert into user_following values ('{currentUserId}', '{target_id}')";
                    MySqlCommand cmd3 = new MySqlCommand(sql3, msc);
                    cmd3.ExecuteNonQuery();

                    string sql4 = $"update user_info set following_count=following_count+1 where id='{currentUserId}'";
                    MySqlCommand cmd4 = new MySqlCommand(sql4, msc);
                    cmd4.ExecuteNonQuery();
                    string sql5 = $"update user_info set follower_count=follower_count+1 where id='{target_id}'";
                    MySqlCommand cmd5 = new MySqlCommand(sql5, msc);
                    cmd5.ExecuteNonQuery();

                }
            }
            return following;
        }

        public void updateAvatar(string avatarUrl, string currentUserId)
        {
            using (MySqlConnection msc = mysqlService.newConnection())
            {
                msc.Open();
                string sql = $"update user_info set avatar_url={avatarUrl} where id={currentUserId}";
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                cmd.ExecuteNonQuery();
            }
        }

        public void updateUsername(string username, string currentUserId)
        {
            using (MySqlConnection msc = mysqlService.newConnection())
            {
                msc.Open();
                string sql = $"update user_info set name={username} where id={currentUserId}";
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                cmd.ExecuteNonQuery();
            }
        }
    }
}