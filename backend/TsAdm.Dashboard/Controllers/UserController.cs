using System.Web.Http;
using TsAdm.Domain;
using TsAdm.Repository;
using TsAdm.Dashboard.Services;
using TsAdm.Dashboard.RequestBodies;

namespace TsAdm.Dashboard.Controllers
{
    public class UserController : ApiController
    {
        //private readonly IUserRepository _userRepository = new UserRepository();

        private Tokenhub tokenhub = new Tokenhub();


        //private SqlService service = new SqlService();
        private Service service = new Service();

        //用户认证-登录-login
        [HttpPost]
        [Route("register")]
        public IHttpActionResult register(RegisterBody body)
        {
            //PicSwitcher pic = new PicSwitcher();
            //pic.storePic();

            string code = service.register(body);
            if (code[0] != '-')
            {
                tokenhub.generateToken(body.id);
                string token = tokenhub.getToken();
                return Ok(code);
            }
            else
            {
                return BadRequest(code);
            }
        }
        
        //用户认证-注册-register
        [HttpPost]
        [Route("login")]
        public IHttpActionResult login(RegisterBody body)
        {
            if (service.login(body))
            {
                tokenhub.generateToken(body.id);
                string token = tokenhub.getToken();
                //string s1 = tokenhub.getId(token);
                //string s2 = tokenhub.getId("QWQ");
                return Ok(token.ToString());
            }
            else
            {
                return BadRequest();
            }
        }

        //用户认证-检查id重复情况-checkid
        [HttpGet]
        [Route("register/checkId")]
        public IHttpActionResult checkId(Query query)
        {
            if (service.checkId(query))
            {
                bool valid=service.checkId(query);
                return Ok(valid.ToString());
            }
            else
            {
                return BadRequest();
            }
        }

        //内容展示与读者交互-获取内容列表
        [HttpGet]
        [Route("content")]
        public IHttpActionResult getContent(Query query)
        {
            if (service.getContents(query))
            {
                bool contents = service.getContents(query);
                return Ok(contents.ToString());
            }
            else
            {
                return BadRequest();
            }
        }

        //内容展示与读者交互-获取特定用户发布的内容列表
        [HttpGet]
        [Route("content")]
        public IHttpActionResult getContentAbstract(Query query)
        {
            if (service.getContentAbstract(query))
            {
                bool contents = service.getContentAbstract(query);
                return Ok(contents.ToString());
            }
            else
            {
                return BadRequest();
            }
        }

        //内容展示与读者交互-获取内容详情
        //路径参数
        [HttpGet]
        [Route("content/{id}")]
        public IHttpActionResult getContent(int id)
        {
            Content contents = service.getContents(id);
            if (contents != null)
            {
                return Ok(contents);
            }
            else
            {
                return BadRequest();
            }

        }

        //内容展示与读者交互-获取用户与当前内容的交互情况
        [HttpGet]
        [Route("content/{id}/interaction")]
        public IHttpActionResult getInteraction(int id)
        {
            ContentInteraction interaction = service.getInteraction(id);
            
           if(interaction != null)
           {
                return Ok(interaction);
           }
            else
            {
                return BadRequest();
            }
        }

        //用户点赞/取消点赞

        //用户收藏/取消收藏

        //内容展示与读者交互-获取内容评论
        [HttpGet]
        [Route("content/{id}/comment")]
        public IHttpActionResult getComment(int id)
        {
            Comment comment = service.getComments(id);
            if (id != null)
            {
                return Ok(comment);
            }
            else
            {
                return BadRequest();
            }
        }

        //（取消）点赞评论


        //用户资料-获取用户信息
        [HttpGet]
        [Route("user/{id}")]
        public IHttpActionResult getUser(int id)
        {
            User user = service.getUsers(id);
            if (id != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest();
            }
        }

        //修改用户信息

        //获取用户的关注列表
        [HttpGet]
        [Route("user/{id}/following")]
        public IHttpActionResult getFollowing(int id)
        {
            User user = service.getFollowings(id);
            if (id != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest();
            }
        }

        //获取用户的粉丝列表
        [HttpGet]
        [Route("user/{id}/following")]
        public IHttpActionResult getFollower(int id)
        {
            User user = service.getFollowers(id);
            if (id != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest();
            }
        }

        //关注/取消关注

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

        //删除内容
        [HttpDelete]
        [Route("content/{id}")]
        public IHttpActionResult DeleteContent(int id)
        {
            if (service.deleteContents(id))
            {
                return Ok(Request.ToString());
            }
            else
            {
                return BadRequest();
            }
        }

        ////发布评论
        //[HttpPost]
        //[Route("content/{id}/comment")]
        //public IHttpActionResult PostComment(Body body)
        //{
            
        //}

        //删除评论
        //[HttpDelete]
        //[Route("content/{content_id}/comment/{comment_id}")]
        //public IHttpActionResult DeleteComment(Path path)
        //{
        //    //if (service.deleteComments(path))
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        /// <summary>
        /// test APIs
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public IHttpActionResult Get()
        //{
        //    return Ok(new { UserName = "Admin", EmailAddress = "master@admin.com" });
        //}  

        //[HttpGet]
        //[Route("api")]
        //public IHttpActionResult GetTodos()
        //{
        //    return Ok(service.getTodos());
        //}

        //[HttpGet]
        //[Route("api/{id}")]
        //public IHttpActionResult GetTodos(int id)
        //{
        //    return Ok(service.getTodos(id));
        //}

        //[HttpPost]
        //[Route("api")]
        //public IHttpActionResult PostTodos(Todos todo)
        //{
        //    return Ok(service.addTodos(todo));
        //}


        //[HttpDelete]
        //[Route("api/{id}")]
        //public IHttpActionResult DeleteTodo(int id)
        //{
        //    service.deleteTodos(id);
        //    return Ok();
        //}

        // The Original codes, which is useless
        //public IHttpActionResult Find(int id)
        //{
        //    Thread.Sleep(new Random().Next(500, 1500));
        //    var model = _userRepository.FindById(id);
        //    return Ok(new { data = model });
        //}

        //[HttpPost]
        //public IHttpActionResult Grid(UserCommand command)
        //{
        //    Thread.Sleep(new Random().Next(500, 1500));
        //    var response = TxResponseExtensions.GetTxResponseModel;
        //    var predicateBuilder = PredicateBuilder.Create<User>(null);
        //    if (!string.IsNullOrEmpty(command.FieldValue))
        //    {
        //        switch (command.FieldName)
        //        {
        //            case "first_name":
        //                predicateBuilder = predicateBuilder.And(x => x.first_name.IndexOf(command.FieldValue.ToString(),StringComparison.CurrentCultureIgnoreCase)>=0);
        //                break;
        //            case "last_name":
        //                predicateBuilder = predicateBuilder.And(x => x.last_name.IndexOf(command.FieldValue.ToString(), StringComparison.CurrentCultureIgnoreCase) >= 0);
        //                break;
        //            case "email":
        //                predicateBuilder = predicateBuilder.And(x => x.email.IndexOf(command.FieldValue.ToString(), StringComparison.CurrentCultureIgnoreCase) >= 0);
        //                break;
        //        }
        //    }
        //    if (!string.Equals(command.Gender, "All", StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        predicateBuilder = predicateBuilder.And(x => x.gender == command.Gender);
        //    }
        //    var lst = _userRepository.FindPagedList(predicateBuilder, command.Sort.Prop, command.Sort.Desc, command.Pagination.CurrentPage - 1, command.Pagination.PageSize);
        //    var grid = new DataSource<User>(lst)
        //    {
        //        Data = lst
        //    };
        //    return Ok(new { response, grid });
        //}
    }
}
