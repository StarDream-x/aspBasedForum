using System.Web.Http;
using TsAdm.Repository;
using TsAdm.Dashboard.Services;
using TsAdm.Dashboard.RequestBodies;
using System.Collections.Generic;
using System.Web;

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
                string token = tokenhub.getToken();
                return Ok(token);
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
                string token = tokenhub.getToken();
                return Ok(token);
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
        public IHttpActionResult getUserInfo([FromUri] string userId)
        {
            string currentUserId = getCurrentUserId();
            UserInfo userInfo = userService.getUserById(userId,currentUserId);
            return Ok(userInfo); 
        }

        //获取用户的收藏列表
        [HttpGet]
        [Route("user/{id}/favorite")]
        public IHttpActionResult getFavorite([FromUri]string userId)
        {
            List<ContentAbstract> contentAbstracts = userService.getUserfavorite(userId);
            return Ok(contentAbstracts);
        }

        //获取用户的关注列表
        [HttpGet]
        [Route("user/{id}/following")]
        public IHttpActionResult getFollowing([FromUri] string userId)
        {
            List<UserInfo> userInfos = userService.getUserFollwee(userId);
            return Ok(userInfos);
        }

        //获取用户的粉丝列表
        [HttpGet]
        [Route("user/{id}/follower")]
        public IHttpActionResult getFollower([FromUri] string userId)
        {
            List<UserInfo> userInfos = userService.getUserFollwer(userId);
            return Ok(userInfos);
        }

        //关注/取消关注
        [HttpPatch]
        [Route("me/follow/{target_id}")]
        public IHttpActionResult userFollow([FromUri] string target_id,[FromBody]bool following)
        {
            string currentUserId = getCurrentUserId();
            bool res = userService.userFollow(currentUserId, target_id, following);
            return Ok(res);
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
