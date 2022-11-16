using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using TsAdm.Dashboard.RequestBodies;

namespace TsAdm.Dashboard.Services
{
    public class ContentService
    {
        private MysqlService mysqlService = new MysqlService();
        private UserService userService = new UserService();
        //获取内容概要
        public List<ContentAbstract> getContents(long prevRequest)
        {
            using (MySqlConnection msc = mysqlService.newConnection())
            {
                msc.Open();
                string sql = $"select content.id, content.title,user_info.id, " +
                    $"user_info.name, content.view_count " +
                    $"from content, user_info " +
                    $"where content.author_id = user_info.id " +
                    $"order by content.id limit {prevRequest * 10}, 10";
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
                    string sql2 = $"select media.url from media, content_media " +
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
        //获取特定用户发布的内容概要
        public List<ContentAbstract> getContentsByUserId(string userId, long contentsRead)
        {
            using (MySqlConnection msc = mysqlService.newConnection())
            {
                msc.Open();
                string sql = $"select content.id, content.title,user_info.id, " +
                    $"user_info.name, content.view_count " +
                    $"from content, user_info " +
                    $"where content.author_id = user_info.id and " +
                    $"content.author_id = '{userId}'" +
                    $"order by content.id limit {contentsRead * 10}, 10";
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
                    string sql2 = $"select media.url from media, content_media " +
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
        //获取内容详情
        public Content getContent(long contentId, string currentUserId)
        {
            using (MySqlConnection msc = mysqlService.newConnection())
            {
                msc.Open();
                string sql = $"select content.id, content.title, author_id, " +
                    $"view_count, like_count, favorite_count, publish_time, body " +
                    $"from content " +
                    $"where content.id = {contentId}";
                //创建命令对象
                MySqlCommand cmd = new MySqlCommand(sql, msc);

                Content content = new Content();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        content.id = reader.GetInt64(0);
                        content.title = reader.GetString(1);
                        content.authorId = reader.GetString(2);
                        content.viewCount = reader.GetInt64(3);
                        content.likeCount = reader.GetInt64(4);
                        content.favoriteCount = reader.GetInt64(5);
                        content.publishTime = reader.GetInt64(6);
                        content.body = reader.GetString(7);
                    }
                }

                string sql2 = $"select type, url from media, content_media " +
                    $"where content_media.media_id = media.id and " +
                    $"content_media.content_id = {contentId}";

                MySqlCommand cmd2 = new MySqlCommand(sql2, msc);

                using (MySqlDataReader reader1 = cmd2.ExecuteReader())
                {
                    List<MediaItem> list = new List<MediaItem>();
                    while (reader1.Read())
                    {
                        MediaItem item = new MediaItem();
                        item.type = reader1.GetString(0);
                        item.imageUrl = item.url = reader1.GetString(1);
                        list.Add(item);
                    }
                    content.media = list.ToArray();
                }

                string sql3 = $"select comment.id, user_info.id, user_info.name, user_info.avatar_url, " +
                    $"user_info.introduction, user_info.following_count, user_info.follower_count, " +
                    $"user_info.favorite_count, comment.content, comment.like_count " +
                    $"from comment, user_info, content_comment " +
                    $"where comment.user_id = user_info.id and " +
                    $"content_comment.content_id = {contentId} and " +
                    $"content_comment.comment_id = comment.id";

                MySqlCommand cmd3 = new MySqlCommand(sql3, msc);

                List<Comment> list2 = new List<Comment>();
                using (MySqlDataReader reader2 = cmd3.ExecuteReader())
                {
                    while (reader2.Read())
                    {
                        Comment comment = new Comment();
                        comment.id = reader2.GetInt64(0);
                        comment.content = reader2.GetString(8);
                        comment.likeCount = reader2.GetInt64(9);
                        comment.publishTime = 0;
                        UserInfo userInfo = new UserInfo();
                        userInfo.id = reader2.GetString(1);
                        userInfo.username = reader2.GetString(2);
                        userInfo.avatarUrl = reader2.GetString(3);
                        userInfo.introduction = reader2.GetString(4);
                        userInfo.followingCount = reader2.GetInt64(5);
                        userInfo.followerCount = reader2.GetInt64(6);
                        userInfo.favoriteCount = reader2.GetInt64(7);
                        comment.user = userInfo;
                        list2.Add(comment);
                    }
                }

                foreach (Comment comment in list2)
                {
                    string sql4 = $"select * from comment_user_liked where comment_id = {comment.id} and user_id = '{currentUserId}'";
                    MySqlCommand cmd4 = new MySqlCommand(sql4, msc);
                    using (MySqlDataReader reader4 = cmd4.ExecuteReader())
                    {
                        comment.like = reader4.Read();
                    }
                }

                foreach (Comment comment in list2)
                {
                    string sql5 = $"select * from user_following where follower_id = '{currentUserId}' and followee_id = '{comment.user.id}'";
                    MySqlCommand cmd5 = new MySqlCommand(sql5, msc);
                    using (MySqlDataReader reader5 = cmd5.ExecuteReader())
                    {
                        comment.user.following = reader5.Read();
                    }
                }
                content.comments = list2.ToArray();

                string sql6 = $"update content set view_count=view_count+1 where id = {contentId}";
                MySqlCommand cmd6 = new MySqlCommand(sql6, msc);
                cmd6.ExecuteNonQuery();

                return content;
            }
        }
        //获取当前用户和文章交互情况
        public ContentInteraction getContentInteraction(long id, string currentUserId)
        {
            using (MySqlConnection msc = mysqlService.newConnection())
            {
                msc.Open();
                string sql = $"select liked, favorite from content_user_interaction where user_id = '{currentUserId}' and content_id = {id}";
                //创建命令对象
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                ContentInteraction contentInteraction = new ContentInteraction();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        contentInteraction.like = reader.GetBoolean(0);
                        contentInteraction.favorite = reader.GetBoolean(1);
                    }
                    return contentInteraction;
                }
            }
        }

        //用户点赞，取消点赞
        public void userInteraction(long id, bool? likeInput, bool? favoriteInput, string currentUserId)
        {
            using (MySqlConnection msc = mysqlService.newConnection())
            {
                msc.Open();
                string sql = $"select liked, favorite from content_user_interaction where user_id = '{currentUserId}' and content_id = {id}";
                //创建命令对象
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                bool exist;
                bool likedBefore = false;
                bool favoritedBefore = false;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    exist = reader.Read();
                    if (exist)
                    {
                        likedBefore = reader.GetBoolean(0);
                        favoritedBefore = reader.GetBoolean(1);
                    }
                }
                bool like = (likeInput == null) ? likedBefore : likeInput.Value;
                bool favorite = (favoriteInput == null) ? favoritedBefore : favoriteInput.Value;
                if (exist)
                {
                    string sql2 = $"update content_user_interaction set liked ={like}, favorite={favorite} where user_id = '{currentUserId}' and content_id = {id}";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, msc);
                    cmd2.ExecuteNonQuery();
                }
                else
                {
                    string sql3 = $"insert into content_user_interaction values ({id},'{currentUserId}',{like},{favorite})";
                    MySqlCommand cmd3 = new MySqlCommand(sql3, msc);
                    cmd3.ExecuteNonQuery();
                }

                int likeDiff = bool2Int(like) - bool2Int(likedBefore);
                int favoriteDiff = bool2Int(favorite) - bool2Int(favoritedBefore);

                string sql4 = $"update content set like_count=like_count+({likeDiff}), favorite_count=favorite_count+({favoriteDiff}) " +
                    $"where id={id}";
                MySqlCommand cmd4 = new MySqlCommand(sql4, msc);
                cmd4.ExecuteNonQuery();

                string sql5 = $"update user_info set favorite_count=favorite_count+({favoriteDiff}) where id = '{currentUserId}'";
                MySqlCommand cmd5 = new MySqlCommand(sql5, msc);
                cmd5.ExecuteNonQuery();
            }
        }

        private int bool2Int(bool b)
        {
            return b ? 1 : 0;
        }

        //点赞评论 存疑 1.要不要存 like在数据库中，如果不存，会不会有问题   2.err.message怎末传递
        public bool userCommentInteract(string currentUserId, long content_id,long comment_id,bool like)
        {
            using (MySqlConnection msc = mysqlService.newConnection())
            {
                msc.Open();
                if (!like)
                {
                    string sql = $"delete from comment_user_liked where comment_id = {comment_id} and " +
                        $"user_id = '{currentUserId}'";
                    MySqlCommand cmd = new MySqlCommand(sql, msc);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    string sql2 = $"select * from comment_user_liked where comment_id = {comment_id} and " +
                        $"user_id = '{currentUserId}'";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, msc);
                    bool exist;
                    using (var reader2 = cmd2.ExecuteReader())
                    {
                        exist = reader2.Read();
                    }
                    if (!exist)
                    {
                        string sql3 = $"insert into comment_user_liked values ({comment_id}, '{currentUserId}')";
                        MySqlCommand cmd3 = new MySqlCommand(sql3, msc);
                        cmd3.ExecuteNonQuery();
                    }
                }
            }
            return like;
        }
    }
}