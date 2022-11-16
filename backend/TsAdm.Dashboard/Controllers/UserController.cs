using System.Web.Http;
using TsAdm.Repository;
using TsAdm.Dashboard.Services;
using TsAdm.Dashboard.RequestBodies;
using System.Collections.Generic;
using System.Web;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System;

namespace TsAdm.Dashboard.Controllers
{
    public class UserController : ApiController
    {
        //private readonly IUserRepository _userRepository = new UserRepository();

        private Tokenhub tokenhub = new Tokenhub();

        private UserService userService = new UserService();
        private ContentService contentService = new ContentService();
        private AuthenticationService authenticationService = new AuthenticationService();
        
        private string getCurrentUserId()
        {
            string value = HttpContext.Current.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(value) && value.StartsWith("token="))
            {
                string token = value.Substring(6);
                Tokenhub tokenhub = new Tokenhub();
                string id = tokenhub.getId(token);
                if (id == "ERROR")
                {
                    return string.Empty;
                }
                else
                {
                    return id;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        [HttpPost]
        [Route("register")]
        public IHttpActionResult register([FromBody] RegisterBody body)
        {
            bool result = authenticationService.register(body);
            if (result)
            {
                tokenhub.generateToken(body.id);
                string token = tokenhub.getToken(body.id);
                Dictionary<string, object> result2 = new Dictionary<string, object>();
                result2.Add("token", token);
                return Ok(result2);
            }
            else
            {
                return BadRequest("error");
            }
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult login([FromBody] LoginBody body)
        {
            if (authenticationService.login(body))
            {
                tokenhub.generateToken(body.Id);
                string token = tokenhub.getToken(body.Id);
                Dictionary<string, object> result = new Dictionary<string, object>();
                result.Add("token", token);
                return Ok(result);
            }
            else
            {
                return BadRequest("error");
            }
        }

        //用户认证-检查id重复情况-checkid
        [HttpGet]
        [Route("register/check_id")]
        public IHttpActionResult checkId(string id)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("valid", authenticationService.checkId(id));
            return Ok(result);
        }

        //用户资料-获取用户信息
        [HttpGet]
        [Route("user/{id}")]
        public IHttpActionResult getUserInfo([FromUri] string id)
        {
            string currentUserId = getCurrentUserId();
            UserInfo userInfo = userService.getUserById(id,currentUserId);
            return Ok(userInfo); 
        }

        //获取用户的收藏列表
        [HttpGet]
        [Route("user/{id}/favorite")]
        public IHttpActionResult getFavorite([FromUri]string id)
        {
            List<ContentAbstract> contentAbstracts = userService.getUserfavorite(id);
            return Ok(contentAbstracts);
        }

        //获取用户的关注列表
        [HttpGet]
        [Route("user/{id}/following")]
        public IHttpActionResult getFollowing([FromUri] string id)
        {
            List<UserInfo> userInfos = userService.getUserFollwee(id);
            return Ok(userInfos);
        }

        //获取用户的粉丝列表
        [HttpGet]
        [Route("user/{id}/follower")]
        public IHttpActionResult getFollower([FromUri] string id)
        {
            List<UserInfo> userInfos = userService.getUserFollwer(id);
            return Ok(userInfos);
        }

        //关注/取消关注
        [HttpPatch]
        [Route("me/follow/{target_id}")]
        public IHttpActionResult userFollow([FromUri] string target_id,[FromBody] Dictionary<string, bool> body)
        {
            if (body.TryGetValue("following", out bool following))
            {
                string currentUserId = getCurrentUserId();
                bool res = userService.userFollow(currentUserId, target_id, following);
                Dictionary<string, bool> result = new Dictionary<string, bool>();
                result.Add("following", res);
                return Ok(result);
            }
            return BadRequest("Malformed request body");
        }

        ////内容发布-发布内容
        //[HttpPost]
        //[Route("content")]
        //public IHttpActionResult PostContent(Body body)
        //{
        //    if (service.postContents(body))
        //    {
        //        bool contents = service.postContents(body);
        //        return Ok(contents.ToString());
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        ////发布评论
        //[HttpPost]
        //[Route("content/{id}/comment")]
        //public IHttpActionResult PostComment(Body body)
        //{

        //}

    }
}
