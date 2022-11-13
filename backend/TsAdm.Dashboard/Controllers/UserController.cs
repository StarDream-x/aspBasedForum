using System;
using System.Linq;
using System.Threading;
using System.Web.Http;
using TsAdm.Core.Extensions;
using TsAdm.Dashboard.Models;
using TsAdm.Dashboard.Models.SearchCommand;
using TsAdm.Dashboard.Util.TsResponse;
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


        [HttpPost]
        [Route("register")]
        public IHttpActionResult register(Body body)
        {
            //PicSwitcher pic = new PicSwitcher();
            //pic.storePic();

            string code = service.register(body);
            if (code[0] != '-')
            {
                tokenhub.generateToken();
                string token = tokenhub.getToken();
                return Ok(code);
            }
            else return BadRequest(code);
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult login(Body body)
        {
            if (service.login(body))
            {
                tokenhub.generateToken();
                string token = tokenhub.getToken();
                return Ok(token.ToString());
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("register/checkId")]
        public IHttpActionResult checkId(Query query)
        {
            bool ck = service.checkId(query);
            if (ck) return Ok(ck.ToString());
            else return BadRequest(ck.ToString());
        }

        [HttpGet]
        [Route("content")]
        public IHttpActionResult getContent(Query query)
        {
            Content[] contents = service.getContents(query);
            //return Content(System.Net.HttpStatusCode.NoContent, "empty");
            //if (contents == null) return Content(System.Net.HttpStatusCode.NoContent, "已全部查询完毕");
            return Ok(contents);
        }

        [HttpGet]
        [Route("content/{id}")]
        public IHttpActionResult getContent(int id)
        {
            Content contents = service.getContents(id);
            return Ok(contents);
        }

        [HttpGet]
        [Route("content/{id}/interaction")]
        public IHttpActionResult getInteraction(int id)
        {
            ContentInteraction interaction = service.getInteraction(id);
            return Ok(interaction);
        }


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
